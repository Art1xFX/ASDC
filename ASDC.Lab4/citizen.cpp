#include "stdafx.h"
#include "citizen.h"


CITIZEN::CITIZEN()
{
	first_name = new char('\0');
	last_name = new char('\0');
	birth = tm();
}

CITIZEN::CITIZEN(FILE * file)
{
	fread(&pin, sizeof(int64_t), 1, file);
	int count;
	fread(&count, sizeof(int), 1, file);
	first_name = new char[count + 1];
	fread(first_name, sizeof(char), count, file);
	first_name[count] = '\0';
	fread(&count, sizeof(int), 1, file);
	last_name = new char[count + 1];
	fread(last_name, sizeof(char), count, file);
	last_name[count] = '\0';
	birth = tm();
	fread(&birth.tm_mday, sizeof(int), 1, file);
	fread(&birth.tm_mon, sizeof(int), 1, file);
	fread(&birth.tm_year, sizeof(int), 1, file);
	fread(&gender, sizeof(int), 1, file);
}


CITIZEN::~CITIZEN()
{
	//delete[] first_name;
	//delete[] last_name;
}
