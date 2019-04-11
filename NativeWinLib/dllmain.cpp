#include "pch.h"

#define DllExport __declspec( dllexport )

extern "C" {

	// return integer
	DllExport int GetInt() {
		return 1234;
	}

	// return string
	DllExport bool GetString(char* data, int buflength) {
		if (buflength < 1) return false;

		for (int i = 0; i < buflength; i++) {
			data[i] = '0' + (i % 10);
		}
		return true;
	}


}