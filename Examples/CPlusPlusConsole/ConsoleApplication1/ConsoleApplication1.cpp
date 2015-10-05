// ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
using  namespace std;

const int col = 3;

void input(int list[][col]){
	cout << "Enter Number: ";
	cin >> list[0][0];
}

int _tmain(int argc, _TCHAR* argv[])
{
	int list[2][3];
	cout << "Hello World";
	input(list);
	cout << "Entered number is: " << list[0][0];
	system("PAUSE");
	return 0;
}



