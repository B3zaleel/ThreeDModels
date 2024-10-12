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

        public static class KHR_animation_pointer
        {
            public const string Pointer = "pointer";
        }

        public static class KHR_lights_punctual
        {
            public static class KhrLightsPunctualLight
            {
                public const string Intensity = "intensity";
                public const string Range = "range";
                public const string Spot = "spot";
            }

            public static class KhrLightsPunctualLightSpot
            {
                public const string InnerConeAngle = "innerConeAngle";
                public const string OuterConeAngle = "outerConeAngle";
            }
        }

        public static class KHR_materials_anisotropy
        {
            public const string AnisotropyStrength = "anisotropyStrength";
            public const string AnisotropyRotation = "anisotropyRotation";
            public const string AnisotropyTexture = "anisotropyTexture";
        }

        public static class KHR_materials_clearcoat
        {
            public const string ClearcoatFactor = "clearcoatFactor";
            public const string ClearcoatTexture = "clearcoatTexture";
            public const string ClearcoatRoughnessFactor = "clearcoatRoughnessFactor";
            public const string ClearcoatRoughnessTexture = "clearcoatRoughnessTexture";
            public const string ClearcoatNormalTexture = "clearcoatNormalTexture";
        }

        public static class KHR_materials_dispersion
        {
            public const string Dispersion = "dispersion";
        }

        public static class KHR_materials_emissive_strength
        {
            public const string EmissiveStrength = "emissiveStrength";
        }

        public static class KHR_materials_ior
        {
            public const string Ior = "ior";
        }

        public static class KHR_materials_iridescence
        {
            public const string IridescenceFactor = "iridescenceFactor";
            public const string IridescenceTexture = "iridescenceTexture";
            public const string IridescenceIor = "iridescenceIor";
            public const string IridescenceThicknessMinimum = "iridescenceThicknessMinimum";
            public const string IridescenceThicknessMaximum = "iridescenceThicknessMaximum";
            public const string IridescenceThicknessTexture = "iridescenceThicknessTexture";
        }

        public static class KHR_materials_sheen
        {
            public const string SheenColorFactor = "sheenColorFactor";
            public const string SheenColorTexture = "sheenColorTexture";
            public const string SheenRoughnessFactor = "sheenRoughnessFactor";
            public const string SheenRoughnessTexture = "sheenRoughnessTexture";
        }

        public static class KHR_materials_specular
        {
            public const string SpecularFactor = "specularFactor";
            public const string SpecularTexture = "specularTexture";
            public const string SpecularColorFactor = "specularColorFactor";
            public const string SpecularColorTexture = "specularColorTexture";
        }

        public static class KHR_materials_transmission
        {
            public const string TransmissionFactor = "transmissionFactor";
            public const string TransmissionTexture = "transmissionTexture";
        }

        public static class KHR_materials_variants
        {
            public const string Variants = "variants";
            public const string Mappings = "mappings";
        }

        public static class KHR_materials_volume
        {
            public const string ThicknessFactor = "thicknessFactor";
            public const string ThicknessTexture = "thicknessTexture";
            public const string AttenuationDistance = "attenuationDistance";
            public const string AttenuationColor = "attenuationColor";
        }

        public static class KHR_texture_basisu
        {
            public const string Source = "source";
        }

        public static class KHR_texture_transform
        {
            public const string Offset = "offset";
        }

        public static class MPEG_accessor_timed
        {
            public const string Immutable = "immutable";
            public const string SuggestedUpdateRate = "suggestedUpdateRate";
        }

        public static class MPEG_animation_timing
        {
            public const string Accessor = "accessor";
        }

        public static class MPEG_audio_spatial
        {
            public const string Sources = "sources";
            public const string Listener = "listener";
            public const string Reverbs = "reverbs";

            public static class MpegAudioSpatialSource
            {
                public const string Id = "id";
                public const string Pregain = "pregain";
                public const string PlaybackSpeed = "playbackSpeed";
                public const string Attenuation = "attenuation";
                public const string AttenuationParameters = "attenuationParameters";
                public const string ReferenceDistance = "referenceDistance";
                public const string ReverbFeed = "reverbFeed";
                public const string ReverbFeedGain = "reverbFeedGain";
            }

            public static class MpegAudioSpatialReverb
            {
                public const string Bypass = "bypass";
                public const string Properties = "properties";
                public const string Predelay = "predelay";
            }

            public static class MpegAudioSpatialReverbProperty
            {
                public const string Frequency = "frequency";
                public const string RT60 = "RT60";
                public const string DSR = "DSR";
            }
        }

        public static class MPEG_buffer_circular
        {
            public const string Media = "media";
            public const string Tracks = "tracks";
        }

        public static class MPEG_media
        {
            public const string Media = "media";

            public static class MpegMedia
            {
                public const string StartTime = "startTime";
                public const string StartTimeOffset = "startTimeOffset";
                public const string EndTimeOffset = "endTimeOffset";
                public const string Autoplay = "autoplay";
                public const string AutoplayGroup = "autoplayGroup";
                public const string Loop = "loop";
                public const string Controls = "controls";
                public const string Alternatives = "alternatives";
            }

            public static class MpegMediaAlternative
            {
                public const string ExtraParams = "extraParams";
            }

            public static class MpegMediaAlternativeTrack
            {
                public const string Track = "track";
                public const string Codecs = "codecs";
            }
        }

        public static class MPEG_mesh_linking
        {
            public const string Correspondence = "correspondence";
            public const string Pose = "pose";
        }
    }
}
