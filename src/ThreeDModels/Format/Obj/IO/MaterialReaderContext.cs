using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO;

internal class MaterialReaderContext
{
    public Material? Material { get; set; }
    public List<Material> Materials { get; }

    public MaterialReaderContext(Material? material, List<Material> materials)
    {
        Material = material;
        Materials = materials;
    }
}
