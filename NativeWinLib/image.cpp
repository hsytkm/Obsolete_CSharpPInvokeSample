#include "pch.h"
#include <iostream>

#define DllExport __declspec( dllexport )

double GetY(double R, double G, double B)
{
	return 0.299 * R + 0.587 * G + 0.114 * B;
}

extern "C" {

	struct ImagePayload {
		int width;
		int height;
		int bytesPerPixel;
		int stride;
		unsigned char* data;
	};

	// return Image All Average Y
	DllExport double GetImageAllY(const ImagePayload *payload) {
		uint64_t sb = 0, sg = 0, sr = 0;

		int width = payload->width;
		int height = payload->height;
		int bytesPerPixel = payload->bytesPerPixel;
		int stride = payload->stride;
		unsigned char *data = payload->data;

		for (int y = 0; y < height * stride; y += stride)
		{
			for (int x = 0; x < width * bytesPerPixel; x += bytesPerPixel)
			{
				int i = y + x;
				sb += data[i];
				sg += data[i + 1];
				sr += data[i + 2];
			}
		}

		double count = (double)(width * height);
		double ab = sb / count;
		double ag = sg / count;
		double ar = sr / count;
		return GetY(ar, ag, ab);
	}

}
