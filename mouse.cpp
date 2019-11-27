
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
		cout << "I makea lot of noise." << endl;
	};	


	int mouse :: distance(int time, int speed)
	{
			
	int distance = time * speed;

	return distance;
	}	


