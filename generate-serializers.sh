#!/bin/bash

# Script to manually generate XmlSerializers for Paywire.NET
# This works around the Microsoft.XmlSerializer.Generator version resolution issue
# See: https://github.com/dotnet/runtime/issues/90913

echo "Generating XmlSerializers for Paywire.NET..."

# Detect the script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd "$SCRIPT_DIR"

# Set paths - try to find the NuGet package
NUGET_ROOT="${NUGET_PACKAGES:-$HOME/.nuget/packages}"
SGEN_VERSION=$(ls -1 "$NUGET_ROOT/microsoft.xmlserializer.generator" 2>/dev/null | grep -E '^[0-9]' | sort -V | tail -1)

if [ -z "$SGEN_VERSION" ]; then
    echo "Error: Microsoft.XmlSerializer.Generator package not found."
    echo "Please run 'dotnet restore' first."
    exit 1
fi

SGEN_DLL="$NUGET_ROOT/microsoft.xmlserializer.generator/$SGEN_VERSION/lib/netstandard2.0/dotnet-Microsoft.XmlSerializer.Generator.dll"
TARGET_ASSEMBLY="Paywire.NET/bin/Release/net8.0/Paywire.NET.dll"
OUTPUT_DIR="Paywire.NET/bin/Release/net8.0"

# Ensure the project is built
echo "Building project..."
dotnet build Paywire.NET/Paywire.NET.csproj --configuration Release

# Try to generate serializers with forced runtime
echo "Attempting to generate serializers..."
dotnet exec --fx-version 8.0.13 "$SGEN_DLL" --assembly "$TARGET_ASSEMBLY" --force --quiet

# Check if it worked
if [ -f "$OUTPUT_DIR/Paywire.NET.XmlSerializers.dll" ]; then
    echo "Success! XmlSerializers.dll was generated."
else
    echo "Failed to generate XmlSerializers.dll"
    echo "Trying alternative approach..."
    
    # Alternative: Use the tool directly without dotnet exec
    "$SGEN_DLL" "$TARGET_ASSEMBLY" --force 2>/dev/null || true
    
    if [ -f "$OUTPUT_DIR/Paywire.NET.XmlSerializers.dll" ]; then
        echo "Success with alternative approach!"
    else
        echo "Unable to generate XmlSerializers.dll directly."
    fi
fi

# Check if the C# source was generated
if [ -f "$OUTPUT_DIR/Paywire.NET.XmlSerializers.cs" ]; then
    echo "Found generated C# source file. Attempting to compile..."
    
    # Find the .NET SDK path
    DOTNET_ROOT="$(dotnet --info | grep 'Base Path' | awk '{print $3}' | head -1)"
    if [ -z "$DOTNET_ROOT" ]; then
        DOTNET_ROOT="$HOME/.dotnet/sdk/$(dotnet --version)"
    fi
    
    # Find CSC compiler
    CSC=$(find "$DOTNET_ROOT" -name "csc.dll" -path "*/Roslyn/bincore/*" 2>/dev/null | head -1)
    if [ -z "$CSC" ]; then
        echo "Error: Could not find C# compiler. Make sure .NET SDK is installed."
        exit 1
    fi
    
    # Build reference list
    echo "Building reference list..."
    REFS=""
    
    # Add framework references
    FRAMEWORK_DIR="$(dotnet --info | grep 'Microsoft.NETCore.App' | awk '{print $2}' | head -1)"
    if [ -d "$FRAMEWORK_DIR" ]; then
        for dll in "$FRAMEWORK_DIR"/*.dll; do
            REFS="$REFS -r:\"$dll\""
        done
    fi
    
    # Add project references
    if [ -f "Paywire.NET/obj/Release/net8.0/Paywire.NET.csproj.AssemblyReference.cache" ]; then
        # Try to extract references from the build
        REFS="$REFS -r:\"$TARGET_ASSEMBLY\""
        REFS="$REFS -r:\"$NUGET_ROOT/restsharp/112.1.0/lib/net8.0/RestSharp.dll\""
    fi
    
    # Compile the serializer
    echo "Compiling serializer assembly..."
    eval "dotnet exec \"$CSC\" -target:library -optimize -nologo -out:\"$OUTPUT_DIR/Paywire.NET.XmlSerializers.dll\" $REFS \"$OUTPUT_DIR/Paywire.NET.XmlSerializers.cs\""
    
    if [ -f "$OUTPUT_DIR/Paywire.NET.XmlSerializers.dll" ]; then
        echo "Success! Compiled XmlSerializers.dll"
    else
        echo "Failed to compile the generated C# source."
    fi
fi