Task("Generate")
    .Does(() =>
{
    DotNetCoreRun("Generator/Generator.csproj", "README.md.template README.md");
});

RunTarget("Generate");