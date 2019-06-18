#include "pch.h"
#include <iostream>
#include <string.h>

#define ON_LINUX	0

#if ON_LINUX
#define DllExport extern "C" 
#else
#ifdef __cplusplus
#define DllExport extern "C" __declspec(dllexport)
#else
#define DllExport __declspec(dllexport)
#endif
#endif

#pragma region Struct

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
#pragma endregion

#pragma region SetStringToCharArray()

bool SetStringToCharArray(char* dest, int destLength, const char* src, int srcLength) {
	if (destLength < 1) return true;

	bool err;
	int copyLength, nullIndex;

	if (destLength + 1 >= srcLength) {
		// 全部収まるパターン
		err = false;
		copyLength = srcLength;
		nullIndex = srcLength;
	}
	else {
		// 全部収まらないパターン
		err = true;
		copyLength = destLength - 1;
		nullIndex = destLength - 1;
	}

	for (int i = 0; i < copyLength; i++) {
		dest[i] = src[i];
	}
	dest[nullIndex] = '\0';
	return err;
}

bool SetStringToCharArray(char* dest, int destLength, const char* src) {
	return SetStringToCharArray(dest, destLength, src, (int)strlen(src));
}

bool SetStringToCharArray(char* dest, int destLength, std::string str) {
	return SetStringToCharArray(dest, destLength, str.c_str(), (int)str.size());
}

#pragma endregion

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
DllExport bool ToUpperFromLib(const char* src, char* dest, int destLength) {
	bool ret = SetStringToCharArray(dest, destLength, src);
	char* p = dest;
	while (*p != '\0')
	{
		*p = toupper(*p);
		p++;
	}
	return ret;
}

// string(Out) 予約文字列
DllExport bool GetMessageFromLib(char* dest, int destLength) {
	return SetStringToCharArray(dest, destLength, "Hello, I'm Library!");
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

// return string
DllExport const char* GetConstStringFromLib() {
	return "This is const char*";
}
