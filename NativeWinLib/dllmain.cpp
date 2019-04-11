// dllmain.cpp : DLL アプリケーションのエントリ ポイントを定義します。
#include "pch.h"

#define DllExport __declspec( dllexport )

extern "C" {

	DllExport int GetInt() {
		return 1234;
	}

}