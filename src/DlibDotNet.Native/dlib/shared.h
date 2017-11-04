enum struct array2d_type : int
{
    UInt8 = 0,
    UInt16,
    Int16,
    Int32,
    Float,
    Double,
    RgbPixel,
    RgbAlphaPixel,
    HsiPixel,
    Matrix,
};

enum struct matrix_element_type : int
{
    UInt8 = 0,
    UInt16,
    UInt32,
    Int8,
    Int16,
    Int32,
    Float,
    Double,
    RgbPixel,
    RgbAlphaPixel,
    HsiPixel
};

enum element_type : int
{
    OpHeatmap = 0,

    OpJet,

    OpArray2dToMat,
    
    OpTrans
};

enum struct interpolation_type : int
{

    NearestNeighbor = 0,

    Bilinear,

    Quadratic

};

enum struct point_mapping_type : int
{

    Rotator = 0,

    Transform,
    
    TransformAffine,
        
    TransformProjective

};

enum struct mlp_kernel_type : int
{

    Kernel1 = 0 // mlp_kernel_1

};

typedef struct
{
    // uint8_t
    uint8_t uint8_t_start;
    uint8_t uint8_t_inc;
    uint8_t uint8_t_end;
    bool use_uint8_t_inc;

    // uint16_t
    uint16_t uint16_t_start;
    uint16_t uint16_t_inc;
    uint16_t uint16_t_end;
    bool use_uint16_t_inc;

    // int8_t
    int8_t int8_t_start;
    int8_t int8_t_inc;
    int8_t int8_t_end;
    bool use_int8_t_inc;

    // int16_t
    int16_t int16_t_start;
    int16_t int16_t_inc;
    int16_t int16_t_end;
    bool use_int16_t_inc;

    // int32_t
    int32_t int32_t_start;
    int32_t int32_t_inc;
    int32_t int32_t_end;
    bool use_int32_t_inc;

    // float
    float float_start;
    float float_inc;
    float float_end;
    bool use_float_inc;

    // double
    double double_start;
    double double_inc;
    double double_end;
    bool use_double_inc;

    bool use_num;
    int num;
} matrix_range_exp_create_param;

#define ERR_OK                                 0
#define ERR_ARRAY_TYPE_NOT_SUPPORT            -1
#define ERR_INPUT_ARRAY_TYPE_NOT_SUPPORT      -2
#define ERR_OUTPUT_ARRAY_TYPE_NOT_SUPPORT     -3
#define ERR_ELEMENT_TYPE_NOT_SUPPORT          -4
#define ERR_INPUT_ELEMENT_TYPE_NOT_SUPPORT    -5
#define ERR_OUTPUT_ELEMENT_TYPE_NOT_SUPPORT   -6
#define ERR_MATRIX_ELEMENT_TYPE_NOT_SUPPORT   -7
// #define ERR_INPUT_OUTPUT_ARRAY_NOT_SAME_SIZE  -8
// #define ERR_INPUT_OUTPUT_MATRIX_NOT_SAME_SIZE -9
#define ERR_MLP_KERNEL_NOT_SUPPORT            -8