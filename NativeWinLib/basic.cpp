#include "pch.h"
#include <iostream>

#ifdef __cplusplus
#define DllExport extern "C" __declspec(dllexport)
#else
#define DllExport __declspec(dllexport)
#endif

struct Buffer {
	const void* Data;
	size_t Length;
};

struct MyRect {
	double X;
	double Y;
	double Width;
	double Height;
};

// return integer
DllExport int GetIntFromLib() {
	return 1234;
}

// Integer(In/Out) 加算
DllExport int AddIntegerFromLib(int x, const int* y) {
	return x + *y;
}
	
// bool(In/Out) 論理積
DllExport bool GetLogicalConjunctionFromLib(bool b1, bool b2) {
	return b1 & b2;
}

// string(In/Out) 大文字化
DllExport void ToUpperFromLib(const char* src, char* dest, int destLength) {
	int len = strlen(src) < destLength ? strlen(src) : destLength;
	for (int i = 0; i <= len; i++) {
		dest[i] = toupper(src[i]);
	}
}

// byte array use unsafe(In)
DllExport int InByteArray1Lib(unsigned char* data, int dataLength) {
	if (dataLength < 4) return 0;
	return (data[3] << 24) | (data[2] << 16) | (data[1] << 8) | data[0];
}

// byte array don't use unsafe(In)
DllExport int InByteArray2Lib(const Buffer *buffer) {
	return InByteArray1Lib((unsigned char*)buffer->Data, static_cast<int>(buffer->Length));
}

DllExport MyRect GetStructFromLib() {
	MyRect rect;
	rect.X = 1.2;
	rect.Y = 3.4;
	rect.Width = 5.6;
	rect.Height = 7.8;
	return rect;
}

MyRect myRect = { 9.8, 7.6, 5.4, 3.21 };
DllExport MyRect& GetStructPtrFromLib() {
	return myRect;
}

// return string
DllExport bool GetStringFromLib(char* dst, int dstlength) {
	if (dstlength < 1) return false;

	for (int i = 0; i < dstlength; i++) {
		dst[i] = '0' + (i % 10);
	}
	return true;
}
