#!/bin/bash
# Fetch Paywire docs via Jina Reader API
#
# Usage:
#   JINA_API_KEY="your-key" ./scripts/fetch-paywire-docs.sh
#
# This script fetches the Paywire API documentation and converts it to
# well-formatted markdown using Jina Reader.

set -e

URL="https://project.paywire.com/dbtranz/docs/OSBP/files/Development.html"
OUTPUT="docs/paywire-api-reference.md"

# Change to project root
cd "$(dirname "$0")/.."

if [ -z "$JINA_API_KEY" ]; then
  echo "Error: JINA_API_KEY environment variable is required"
  echo "Usage: JINA_API_KEY=\"your-key\" ./scripts/fetch-paywire-docs.sh"
  exit 1
fi

mkdir -p docs

echo "Fetching Paywire documentation from: $URL"

# Fetch and convert via Jina Reader API
curl -sS "https://r.jina.ai/$URL" \
  -H "Authorization: Bearer $JINA_API_KEY" \
  -o "$OUTPUT"

echo ""
echo "Saved to: $OUTPUT"
echo "Size: $(wc -c < "$OUTPUT" | tr -d ' ') bytes"
echo "Lines: $(wc -l < "$OUTPUT" | tr -d ' ')"
