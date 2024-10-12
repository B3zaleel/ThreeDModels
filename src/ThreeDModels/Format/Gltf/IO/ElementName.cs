namespace ThreeDModels.Format.Gltf.IO;

internal static class ElementName
{
    public static class Gltf
    {
        public const string ExtensionsUsed = "extensionsUsed";
        public const string ExtensionsRequired = "extensionsRequired";
        public const string Accessors = "accessors";
        public const string Animations = "animations";
        public const string Asset = "asset";
        public const string Buffers = "buffers";
        public const string BufferViews = "bufferViews";
        public const string Cameras = "cameras";
        public const string Images = "images";
        public const string Materials = "materials";
        public const string Meshes = "meshes";
        public const string Nodes = "nodes";
        public const string Samplers = "samplers";
        public const string Scene = "scene";
        public const string Scenes = "scenes";
        public const string Skins = "skins";
        public const string Textures = "textures";
        public const string Extensions = "extensions";
        public const string Extras = "extras";
    }

    public static class Accessor
    {
        public const string BufferView = "bufferView";
        public const string ByteOffset = "byteOffset";
        public const string ComponentType = "componentType";
        public const string Normalized = "normalized";
        public const string Count = "count";
        public const string Type = "type";
        public const string Max = "max";
        public const string Min = "min";
        public const string Sparse = "sparse";
        public const string Name = "name";
    }

    public static class AccessorSparse
    {
        public const string Indices = "indices";
        public const string Values = "values";
    }

    public static class Animation
    {
        public const string Channels = "channels";
        public const string Samplers = "samplers";
    }

    public static class AnimationChannel
    {
        public const string Sampler = "sampler";
        public const string Target = "target";
    }

    public static class AnimationChannelTarget
    {
        public const string Node = "node";
        public const string Path = "path";
    }

    public static class AnimationSampler
    {
        public const string Input = "input";
        public const string Interpolation = "interpolation";
        public const string Output = "output";
    }

    public static class Asset
    {
        public const string Copyright = "copyright";
        public const string Generator = "generator";
        public const string Version = "version";
        public const string MinVersion = "minVersion";
    }

    public static class Buffer
    {
        public const string Uri = "uri";
        public const string ByteLength = "byteLength";
    }

    public static class BufferView
    {
        public const string Buffer = "buffer";
        public const string ByteStride = "byteStride";
    }

    public static class Camera
    {
        public const string Orthographic = "orthographic";
        public const string Perspective = "perspective";
    }

    public static class CameraOrthographic
    {
        public const string XMag = "xmag";
        public const string YMag = "ymag";
        public const string ZFar = "zfar";
        public const string ZNear = "znear";
    }

    public static class CameraPerspective
    {
        public const string AspectRatio = "aspectRatio";
        public const string YFov = "yfov";
    }

    public static class Image
    {
        public const string MimeType = "mimeType";
    }

    public static class Material
    {
        public const string PbrMetallicRoughness = "pbrMetallicRoughness";
        public const string NormalTexture = "normalTexture";
        public const string OcclusionTexture = "occlusionTexture";
        public const string EmissiveTexture = "emissiveTexture";
        public const string EmissiveFactor = "emissiveFactor";
        public const string AlphaMode = "alphaMode";
        public const string AlphaCutoff = "alphaCutoff";
        public const string DoubleSided = "doubleSided";
    }

    public static class MaterialOcclusionTextureInfo
    {
        public const string Strength = "strength";
    }

    public static class MaterialPbrMetallicRoughness
    {
        public const string BaseColorFactor = "baseColorFactor";
        public const string BaseColorTexture = "baseColorTexture";
        public const string MetallicFactor = "metallicFactor";
        public const string RoughnessFactor = "roughnessFactor";
        public const string MetallicRoughnessTexture = "metallicRoughnessTexture";
    }

    public static class Mesh
    {
        public const string Primitives = "primitives";
        public const string Weights = "weights";
    }

    public static class MeshPrimitive
    {
        public const string Attributes = "attributes";
        public const string Indices = "indices";
        public const string Material = "material";
        public const string Mode = "mode";
        public const string Targets = "targets";
    }

    public static class Node
    {
        public const string Camera = "camera";
        public const string Children = "children";
        public const string Skin = "skin";
        public const string Matrix = "matrix";
        public const string Mesh = "mesh";
        public const string Rotation = "rotation";
        public const string Scale = "scale";
        public const string Translation = "translation";
        public const string Weights = "weights";
    }

    public static class Sampler
    {
        public const string MagFilter = "magFilter";
        public const string MinFilter = "minFilter";
        public const string WrapS = "wrapS";
        public const string WrapT = "wrapT";
    }

    public static class Skin
    {
        public const string InverseBindMatrices = "inverseBindMatrices";
        public const string Skeleton = "skeleton";
        public const string Joints = "joints";
    }

    public static class Texture
    {
        public const string Source = "source";
    }

    public static class TextureInfo
    {
        public const string Index = "index";
        public const string TexCoord = "texCoord";
    }

    public static class Extensions
    {
        public static class ADOBE_materials_clearcoat_specular
        {
            public const string ClearcoatIor = "clearcoatIor";
            public const string ClearcoatSpecularFactor = "clearcoatSpecularFactor";
            public const string ClearcoatSpecularTexture = "clearcoatSpecularTexture";
        }

        public static class ADOBE_materials_clearcoat_tint
        {
            public const string ClearcoatTintFactor = "clearcoatTintFactor";
            public const string ClearcoatTintTexture = "clearcoatTintTexture";
        }

        public static class ADOBE_materials_thin_transparency
        {
            public const string TransmissionFactor = "transmissionFactor";
            public const string TransmissionTexture = "transmissionTexture";
            public const string Ior = "ior";
        }

        public static class AGI_articulations
        {
            public const string Articulations = "articulations";
            public const string IsAttachPoint = "isAttachPoint";
            public const string ArticulationName = "articulationName";

            public static class AgiArticulation
            {
                public const string Stages = "stages";
                public const string PointingVector = "pointingVector";
            }

            public static class AgiArticulationStage
            {
                public const string MinimumValue = "minimumValue";
                public const string MaximumValue = "maximumValue";
                public const string InitialValue = "initialValue";
            }
        }

        public static class AGI_stk_metadata
        {
            public const string SolarPanelGroups = "solarPanelGroups";
            public const string SolarPanelGroupName = "solarPanelGroupName";
            public const string NoObscuration = "noObscuration";

            public static class AgiStkSolarPanelGroup
            {
                public const string Efficiency = "efficiency";
            }
        }

        public static class EXT_lights_ies
        {
            public const string Lights = "lights";
            public const string Light = "light";
            public const string Multiplier = "multiplier";
            public const string Color = "color";
        }

        public static class EXT_lights_image_based
        {
            public const string Intensity = "intensity";
        }

        public static class EXT_mesh_manifold
        {
            public const string ManifoldPrimitive = "manifoldPrimitive";
            public const string MergeIndices = "mergeIndices";
            public const string MergeValues = "mergeValues";
        }

        public static class EXT_meshopt_compression
        {
            public const string Fallback = "fallback";
            public const string Filter = "filter";
        }

        public static class FB_geometry_metadata
        {
            public const string VertexCount = "vertexCount";
            public const string PrimitiveCount = "primitiveCount";
            public const string SceneBounds = "sceneBounds";
        }
    }
}
