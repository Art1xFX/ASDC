#pragma once

class CITIZEN
{
public:

	int64_t pin;
	char* first_name;
	char* last_name;
	tm birth;
	GENDER gender;

	CITIZEN();
	CITIZEN(FILE* file);
	~CITIZEN();
};

