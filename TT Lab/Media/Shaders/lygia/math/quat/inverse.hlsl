#include "div.hlsl"
#include "conj.hlsl"
#include "lengthSq.hlsl"

/*
contributors: Patricio Gonzalez Vivo
description: "Quaternion inverse \n"
use: <QUAT> quatDiv(<QUAT> a, <QUAT> b)
license:
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Prosperity License - https://prosperitylicense.com/versions/3.0.0
    - Copyright (c) 2021 Patricio Gonzalez Vivo under Patron License - https://lygia.xyz/license
*/

#ifndef FNC_QUATINVERSE
#define FNC_QUATINVERSE
QUAT quatInverse(QUAT q) { return quatDiv(quatConj(q), quatLengthSq(q)); }
#endif