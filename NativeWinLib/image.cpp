#include "pch.h"
#include <iostream>

#ifdef __cplusplus
#define DllExport extern "C" __declspec(dllexport)
#else
#define DllExport __declspec(dllexport)
#endif

double GetY(double R, double G, double B)
{
	return 0.299 * R + 0.587 * G + 0.114 * B;
}

struct ImagePayload {
	int width;
	int height;
	int bytesPerPixel;
	int stride;
	unsigned char* data;
};

// return Image All Average Y
DllExport double GetImageAllY(const ImagePayload *payload) {
	//unsigned char *data = payload->data;

	uint64_t sb = 0, sg = 0, sr = 0;
	for (int y = 0; y < payload->height * payload->stride; y += payload->stride)
	{
		for (int i = y; i < y + (payload->width * payload->bytesPerPixel); i += payload->bytesPerPixel)
		{
			sb += payload->data[i];
			sg += payload->data[i + 1];
			sr += payload->data[i + 2];
		}
	}

	double count = (double)payload->width * payload->height;
	return GetY(sr / count, sg / count, sb / count);
}
