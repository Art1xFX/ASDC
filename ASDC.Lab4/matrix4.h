#pragma once
#include "_matrix4.h"
#include "resource.h"

template<typename T>
/// <summary>
/// ������������ ������ �������������� ������������ ������ ��������, ��������� �� �������.
/// </summary>
class matrix4 : public _matrix4<T>
{
	int** index;
	size_t* length;
protected:
	T* _vector;

public:
	/// <summary>
	/// �������������� ����� ������ ��������� ������������� ������� <see cref="matrix4"/> �� �������� ���������� ���������.
	/// </summary>
	/// <param name='i1l'>������ ������� ������� ��������� ������������ �������.</param>
	/// <param name='i1h'>������� ������� ������� ��������� ������������ �������</param>
	/// <param name='i2l'>������ ������� ������� ��������� ������������ �������.</param>
	/// <param name='i2h'>������� ������� ������� ��������� ������������ �������.</param>
	/// <param name='i3l'>������ ������� �������� ��������� ������������ �������.</param>
	/// <param name='i3h'>������� ������� �������� ��������� ������������ �������.</param>
	/// <param name='i4l'>������ ������� ��������� ��������� ������������ �������.</param>
	/// <param name='i4h'>������� ������� ��������� ��������� ������������ �������.</param>
	/// <exception cref="std::invalid_argument">
	/// �������� ��������� <paramref name="i1h"/> ������ <paramref name="i1l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i2h"/> ������ <paramref name="i2l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i3h"/> ������ <paramref name="i3l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i4h"/> ������ <paramref name="i4l"/>.
	/// </exception>
	matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h);

	/// <summary>
	/// �������������� ����� ��������� ������������� ������� <see cref="matrix4"/> �� �������� ���������� ���������, ������� �������� ��������, ������������� �� ���������� �������.
	/// </summary>	
	/// <param name='i1l'>������ ������� ������� ��������� ������������ �������.</param>
	/// <param name='i1h'>������� ������� ������� ��������� ������������ �������</param>
	/// <param name='i2l'>������ ������� ������� ��������� ������������ �������.</param>
	/// <param name='i2h'>������� ������� ������� ��������� ������������ �������.</param>
	/// <param name='i3l'>������ ������� �������� ��������� ������������ �������.</param>
	/// <param name='i3h'>������� ������� �������� ��������� ������������ �������.</param>
	/// <param name='i4l'>������ ������� ��������� ��������� ������������ �������.</param>
	/// <param name='i4h'>������� ������� ��������� ��������� ������������ �������.</param>
	/// <param name='array'>������, �������� �������� ���������� � ����� ������������ ������.</param>
	/// <param name="length">���������� ��������� � ������� <paramref name="array"/></param>
	/// <exception cref="std::invalid_argument">
	/// �������� ��������� <paramref name="i1h"/> ������ <paramref name="i1l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i2h"/> ������ <paramref name="i2l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i3h"/> ������ <paramref name="i3l"/>.
	/// -��� -
	///	�������� ��������� <paramref name="i4h"/> ������ <paramref name="i4l"/>.
	/// -��� -
	/// �������� ��������� <paramref name="array"/> ����� nullptr.
	/// -��� -
	/// �������� ��������� <paramref name="length"/> ������ ����.
	/// </exception>
	matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T* array, size_t length);

	/// <summary>
	/// ����������� ��� �������, ������� <see cref="matrix4"/>.
	/// </summary>
	~matrix4();

	/// <summary>
	/// ���������� ��� ������ ������� �� ��������� ��������.
	/// </summary>
	/// <param name='i1'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i2'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i3'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i4'>�������� ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <returns>������ �� �������, ������������� �� ��������� ��������.</returns>
	/// <exception cref="std::out_of_range">�������� �������� ��������� �� ��������� ����������� ��������� <see cref="getLowerBound"/> � <see cref="getUpperBound"/>.</exception>
	virtual T& at(int i1, int i2, int i3, int i4) = 0;

	/// <summary>
	/// �������� ����� ����� ��������� �� ���� ���������� ������� <see cref="matrix4"/>.
	/// </summary>
	size_t getLength();

	/// <summary>
	/// ���������� �����, �������������� ���������� ��������� � �������� ��������� ������� <see cref="matrix4"/>.
	/// </summary>
	/// <param name='dimension'>��������� ������� <see cref="matrix4"/>, ���������� �������� ���������� � �������, ��� �������� ��������� ���������� �����.</param>
	/// <returns>32-������� ����� ����� ��� �����, �������������� ���������� ��������� � �������� ���������.</returns>
	/// <exception cref="std::out_of_range">
	/// �������� ��������� <paramref name="dimension"/> ������ ����.
	/// - ��� -
	/// �������� ��������� <paramref name="dimension"/> ������ ������.
	/// </exception>
	int getLength(int dimension);


	/// <summary>
	/// ���������� �����, �������������� ���������� �������� �������� ��� ���������� ������ ��������.
	/// </summary>
	/// <returns>32-������� ����� ����� ��� �����, �������������� ���������� �������� �������� ��� ���������� ������ ��������.</returns>
	virtual int getAddCount() = 0;

	/// <summary>
	/// ���������� �����, �������������� ���������� �������� ��������� ��� ���������� ������ ��������.
	/// </summary>
	/// <returns>32-������� ����� ����� ��� �����, �������������� ���������� �������� ��������� ��� ���������� ������ ��������.</returns>
	virtual int getMulCount() = 0;

	/// <summary>
	/// �������� ������ ������� �������� ��������� ��������� � �������.
	/// </summary>
	/// <param name='dimension'>��������� �������, ���������� �������� ���������� � �������, ��� �������� ���������� ���������� ������ �������.</param>
	/// <returns>������ ������� �������� ��������� ��������� � �������.</returns>
	/// <exception cref="std::out_of_range">
	/// �������� ��������� <paramref name="dimension"/> ������ ����.
	/// - ��� -
	/// �������� ��������� <paramref name="dimension"/> ������ ������.
	/// </exception>
	int getLowerBound(int dimension);

	/// <summary>
	/// �������� ������ ���������� �������� ��������� ��������� � �������.
	/// </summary>
	/// <param name='dimension'>��������� �������, ���������� �������� ���������� � �������, ��� �������� ���������� ���������� ������� �������.</param>
	/// <returns>������ ���������� �������� ���������� ��������� � �������.</returns>
	/// <exception cref="std::out_of_range">
	/// �������� ��������� <paramref name="dimension"/> ������ ����.
	/// - ��� -
	/// �������� ��������� <paramref name="dimension"/> ������ ������.
	/// </exception>
	int getUpperBound(int dimension);

private:
	/*virtual int getDimension(int index) = 0;*/
};

template<typename T>
inline matrix4<T>::matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h)
{
	if (i1l > i1h)
		throw std::invalid_argument(MESSAGE_INVALID_ARGUMENT_I1);
	if (i2l > i2h)
		throw std::invalid_argument(MESSAGE_INVALID_ARGUMENT_I2);
	if (i3l > i3h)
		throw std::invalid_argument(MESSAGE_INVALID_ARGUMENT_I3);
	if (i4l > i4h)
		throw std::invalid_argument(MESSAGE_INVALID_ARGUMENT_I4);
	index = new int*[4];
	for (size_t i = 0; i < 4; i++)
		index[i] = new int[2];
	index[0][0] = i1l;
	index[0][1] = i1h;
	index[1][0] = i2l;
	index[1][1] = i2h;
	index[2][0] = i3l;
	index[2][1] = i3h;
	index[3][0] = i4l;
	index[3][1] = i4h;
	length = new size_t[5];
	for (size_t i = 0; i < 4; i++)
		length[i + 1] = abs(index[i][1] - index[i][0]) + 1;
	length[0] = length[1] * length[2] * length[3] * length[4];
	_vector = new T[length[0]];
}

template<typename T>
inline matrix4<T>::matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T * array, size_t length) : matrix4(i1l, i1h, i2l, i2h, i3l, i3h, i4l, i4h)
{
	if (array == nullptr)
		throw std::invalid_argument(MESSAGE_INVALID_ARGUMENT_ARRAY);
	int l = length < this->length[0] ? length : this->length[0];
	for (int i = 0; i < l; i++)
		_vector[i] = array[i];
}

template<typename T>
inline matrix4<T>::~matrix4()
{
	for (size_t i = 0; i < 4; i++)
		delete[] index[i];
	delete[] index;
	delete[] _vector;
}

template<typename T>
inline size_t matrix4<T>::getLength()
{
	return length[0];
}

template<typename T>
inline int matrix4<T>::getLength(int dimension)
{
	if (dimension < 1 || dimension > 4)
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_DIMENSION);
	return length[dimension];
}

template<typename T>
inline int matrix4<T>::getLowerBound(int dimension)
{
	if (dimension < 1 || dimension > 4)
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_DIMENSION);
	return index[dimension - 1][0];
}

template<typename T>
inline int matrix4<T>::getUpperBound(int dimension)
{
	if (dimension < 1 || dimension > 4)
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_DIMENSION);
	return index[dimension - 1][1];
}



