
#include "mouse.h"
#include <iostream>
#include <string>

using namespace std;

	void mouse :: run(int speed)
	{
		cout << "I can run at " << speed << " kmh." << endl;
	};

	void mouse :: noise()
	{
		cout << "I also make a squeaky noise." << endl;
	};	


	void mouse :: eatcheese()
	{
		cout << "Yummy." << endl;
	};

	int mouse :: distance(int time, int speed)
	{
			
	int distance = time * speed;

	return distance;
	}	


