namespace ThreeDModels.Format.Obj.IO.Commands;

delegate void ICommandRead(List<string> commandLine, ObjReaderContext context, ref Obj obj);

internal interface ICommand
{
    abstract static string Name { get; }
    abstract static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj);
    abstract static void Write();
}
