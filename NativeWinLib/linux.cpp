// Linux Source code
#if 0
#include <iostream>

extern "C" {
	int GetInt() {
		return 4321;
	}

	// return string
	bool GetString(char* data, int buflength) {
		if (buflength < 1) return false;

		for (int i = 0; i < buflength; i++) {
			data[i] = '0' + (i % 10);
		}
		return true;
	}
}
#endif
