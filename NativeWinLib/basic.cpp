#include "pch.h"
#include <iostream>

#define DllExport __declspec( dllexport )

extern "C" {

	// return integer
	DllExport int GetIntFromLib() {
		return 1234;
	}

	// return string
	DllExport bool GetStringFromLib(char* data, int buflength) {
		if (buflength < 1) return false;

		for (int i = 0; i < buflength; i++) {
			data[i] = '0' + (i % 10);
		}
		return true;
	}

	// return bool
	DllExport bool GetBoolFromLib(int x) {
		return (x & 1) ? true : false;
	}

	
}