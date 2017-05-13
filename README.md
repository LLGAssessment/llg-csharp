# llg-csharp
A last letter game benchmark implemented in php

## How to run
Clone repository recursively:

```bash
git clone --recursive https://github.com/LLGAssessment/llg-csharp.git
```

Then build

```bash
cd llg-csharp
dotnet restore
dotnet build -c Release
```

and run a test

```bash
time dotnet bin/Release/netcoreapp1.1/llg-csharp.dll < llg-dataset/70pokemons.txt
```
