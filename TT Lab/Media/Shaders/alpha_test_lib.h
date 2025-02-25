
#define CMPF_ALWAYS_FAIL 0
#define CMPF_ALWAYS_PASS 1
#define CMPF_LESS 2
#define CMPF_LESS_EQUAL 3
#define CMPF_EQUAL 4
#define CMPF_NOT_EQUAL 5
#define CMPF_GREATER_EQUAL 6
#define CMPF_GREATER 7

bool Alpha_Func(int func, float alphaRef, float alphaValue)
{
    // ES2 does not have switch
    if(func == CMPF_ALWAYS_PASS)
        return true;
    else if(func == CMPF_LESS)
        return alphaValue < alphaRef;
    else if(func == CMPF_LESS_EQUAL)
        return alphaValue <= alphaRef;
    else if(func == CMPF_EQUAL)
        return alphaValue == alphaRef;
    else if(func == CMPF_NOT_EQUAL)
        return alphaValue != alphaRef;
    else if(func == CMPF_GREATER_EQUAL)
        return alphaValue >= alphaRef;
    else if(func == CMPF_GREATER)
        return alphaValue > alphaRef;

    // CMPF_ALWAYS_FAIL and default
    return false;
}


void Alpha_Test(float func, float alphaRef, vec4 texel)
{
    bool pass_ = Alpha_Func(int(func), alphaRef, texel.a);
    if (!pass_)
        discard;
}