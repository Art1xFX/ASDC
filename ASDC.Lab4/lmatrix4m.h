#pragma once
#include "matrix4.h"
#include "resource.h"

template<typename T>
/// <summary>
/// ������������ ������ �������������� ������������ ������ ��������, ������������� �� �������, ��������� �� �������, ������������ ������������ ������.
/// </summary>
class lmatrix4m : public matrix4<T>
{
	int* _dimension;
	int _dimensionSum;

public:
	/// <summary>
	/// �������������� ����� ������ ��������� ������������� ������� <see cref="lmatrix4m"/> �� �������� ���������� ���������.
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
	lmatrix4m(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h);

	/// <summary>
	/// �������������� ����� ��������� ������������� ������� <see cref="lmatrix4m"/> �� �������� ���������� ���������, ������� �������� ��������, ������������� �� ���������� �������.
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
	lmatrix4m(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T* array, size_t length);

	/// <summary>
	/// ����������� ��� �������, ������� <see cref="lmatrix4m"/>.
	/// </summary>
	~lmatrix4m();

	/// <summary>
	/// ���������� ��� ������ ������� �� ��������� ��������.
	/// </summary>
	/// <param name='i1'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i2'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i3'>������ ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <param name='i4'>�������� ������ ��������, ������� ���������� �������� ��� ������.</param>
	/// <returns>������ �� �������, ������������� �� ��������� ��������.</returns>
	/// <exception cref="std::out_of_range">�������� �������� ��������� �� ��������� ����������� ��������� <see cref="getLowerBound"/> � <see cref="getUpperBound"/>.</exception>
	T& at(int i1, int i2, int i3, int i4);

	/// <summary>
	/// ���������� �����, �������������� ���������� �������� �������� ��� ���������� ������ ��������.
	/// </summary>
	/// <returns>32-������� ����� ����� ��� �����, �������������� ���������� �������� �������� ��� ���������� ������ ��������.</returns>
	int getAddCount();

	/// <summary>
	/// ���������� �����, �������������� ���������� �������� ��������� ��� ���������� ������ ��������.
	/// </summary>
	/// <returns>32-������� ����� ����� ��� �����, �������������� ���������� �������� ��������� ��� ���������� ������ ��������.</returns>
	int getMulCount();

private:
	int getDimension(int dimension);
};

template<typename T>
inline lmatrix4m<T>::lmatrix4m(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h) : matrix4<T>::matrix4(i1l, i1h, i2l, i2h, i3l, i3h, i4l, i4h)
{
	_dimension = new int[4];
	_dimension[3] = 1;
	for (int i = 2; i >= 0; i--)
		_dimension[i] = _dimension[i + 1] * matrix4<T>::getLength(i + 2);
	_dimensionSum = _dimension[0] * this->getLowerBound(1) + _dimension[1] * this->getLowerBound(2) + _dimension[2] * this->getLowerBound(3) + _dimension[3] * this->getLowerBound(4);
}

template<typename T>
inline lmatrix4m<T>::lmatrix4m(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T * array, size_t length) : matrix4<T>::matrix4(i1l, i1h, i2l, i2h, i3l, i3h, i4l, i4h, array, length)
{
	_dimension = new int[4];
	_dimension[3] = 1;
	for (int i = 2; i >= 0; i--)
		_dimension[i] = _dimension[i + 1] * matrix4<T>::getLength(i + 2);
	_dimensionSum = _dimension[0] * this->getLowerBound(1) + _dimension[1] * this->getLowerBound(2) + _dimension[2] * this->getLowerBound(3) + _dimension[3] * this->getLowerBound(4);
}

template<typename T>
inline lmatrix4m<T>::~lmatrix4m()
{
	delete[] _dimension;
}

template<typename T>
inline T & lmatrix4m<T>::at(int i1, int i2, int i3, int i4)
{
	if (i1 < matrix4<T>::getLowerBound(1) || i1 > matrix4<T>::getUpperBound(1))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I1);
	if (i2 < matrix4<T>::getLowerBound(2) || i2 > matrix4<T>::getUpperBound(2))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I2);
	if (i3 < matrix4<T>::getLowerBound(3) || i3 > matrix4<T>::getUpperBound(3))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I3);
	if (i4 < matrix4<T>::getLowerBound(4) || i4 > matrix4<T>::getUpperBound(4))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I4);
	return  matrix4<T>::_vector[i1 * getDimension(1) + i2 * getDimension(2) + i3 * getDimension(3) + i4 * getDimension(4) - _dimensionSum];
}

template<typename T>
inline int lmatrix4m<T>::getAddCount()
{
	return 4;
}

template<typename T>
inline int lmatrix4m<T>::getDimension(int dimension)
{
	return _dimension[dimension - 1];
}

template<typename T>
inline int lmatrix4m<T>::getMulCount()
{
	return 4;
}
