Task("Generate")
    .Does(() =>
{
    DotNetRun("Generator/Generator.csproj", "README.md.template README.md");
});

RunTarget("Generate");