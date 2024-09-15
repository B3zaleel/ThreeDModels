namespace ThreeDModels.Format.Obj.IO.Commands;

delegate void IMaterialCommandRead(List<string> commandLine, MaterialReaderContext context);

internal interface IMaterialCommand
{
    abstract static string Name { get; }
    abstract static void Read(List<string> commandLine, MaterialReaderContext context);
    abstract static void Write();
}
