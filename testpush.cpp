//Test github program fo command line practice - please delete when required
//
#include "mouse.h"
#include <iostream>

using namespace std;

int main()
{

string hello = "hello";

cout << hello << endl; 
cout << "I am a mouse" << endl;

int speed;
int time;

cout << " ... how fast can I run (e.g. 10)? : ";
cin >> speed;

mouse mouse;
mouse.run(speed);
mouse.noise();

cout << " ... how many hrs can I run (e.g. 2): ";
cin >> time;
cout << " I have ran " << mouse.distance(time, speed) << " km." << endl;

cout << "give me something to do ..." << endl;


//add you new lines below to test pushing etc. Let's see what story we can create !!

}



