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

cout << " ... how fast can I run (e.g. 10)? : ";
cin >> speed;

mouse mouse;
mouse.run(speed);
mouse.squeak();


//add you new lines below to test pushing etc. Let's see what story we can create !!

}



