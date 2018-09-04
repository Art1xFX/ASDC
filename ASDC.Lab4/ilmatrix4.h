#pragma once
#include "matrix4.h"

template<typename T>
/// <summary>
/// ������������ ������ �������������� ������������ ������ ��������, ������������� �� �������, ��������� �� �������, ������������ ������ �������.
/// </summary>
class ilmatrix4 : public matrix4<T>
{
	T**** iliffeVector;

public:
	/// <summary>
	/// �������������� ����� ������ ��������� ������������� ������� <see cref="icmatrix4"/> �� �������� ���������� ���������.
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
	ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h);

	/// <summary>
	/// �������������� ����� ��������� ������������� ������� <see cref="icmatrix4"/> �� �������� ���������� ���������, ������� �������� ��������, ������������� �� ���������� �������.
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
	ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T* array, size_t length);

	/// <summary>
	/// ����������� ��� �������, ������� <see cref="icmatrix4"/>.
	/// </summary>
	~ilmatrix4();

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

	int getComplexity();

private:
	int getDimension(int index);
};

template<typename T>
inline ilmatrix4<T>::ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h) : matrix4<T>::matrix4(i1l, i1h, i2l, i2h, i3l, i3h, i4l, i4h)
{
	int offset = 0;
	iliffeVector = new T***[matrix4<T>::getLength(1)] - i1l;
	for (int i1 = i1l; i1 <= i1h; i1++)
	{
		iliffeVector[i1] = new T**[matrix4<T>::getLength(2)] - i2l;
		for (int i2 = i2l; i2 <= i2h; i2++)
		{
			iliffeVector[i1][i2] = new T*[matrix4<T>::getLength(3)] - i3l;
			for (int i3 = i3l; i3 <= i3h; i3++)
			{
				iliffeVector[i1][i2][i3] = &(matrix4<T>::_vector[offset]) - i4l;
				offset += matrix4<T>::getLength(4);
			}
		}
	}
}

template<typename T>
inline ilmatrix4<T>::ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T * array, size_t length) : matrix4<T>::matrix4(i1l, i1h, i2l, i2h, i3l, i3h, i4l, i4h, array, length)
{
	int offset = 0;
	iliffeVector = new T***[matrix4<T>::getLength(1)] - i1l;
	for (int i1 = i1l; i1 <= i1h; i1++)
	{
		iliffeVector[i1] = new T**[matrix4<T>::getLength(2)] - i2l;
		for (int i2 = i2l; i2 <= i2h; i2++)
		{
			iliffeVector[i1][i2] = new T*[matrix4<T>::getLength(3)] - i3l;
			for (int i3 = i3l; i3 <= i3h; i3++)
			{
				iliffeVector[i1][i2][i3] = &(matrix4<T>::_vector[offset]) - i4l;
				offset += matrix4<T>::getLength(4);
			}
		}
	}
}

template<typename T>
inline ilmatrix4<T>::~ilmatrix4()
{
	for (int i1 = index[0][0]; i1 <= index[0][1]; i1++)
	{
		for (int i2 = index[1][0]; i2 <= index[1][1]; i2++)
		{
			for (int i3 = index[2][0]; i3 <= index[2][1]; i3++)
				delete[] iliffeVector[i1][i2][i3];
			delete[] iliffeVector[i1][i2];
		}
		delete[] iliffeVector[i1];
	}
	delete[] iliffeVector;
}

template<typename T>
inline int ilmatrix4<T>::getAddCount()
{
	return 0;
}

template<typename T>
inline int ilmatrix4<T>::getMulCount()
{
	return 0;
}

template<typename T>
inline T & ilmatrix4<T>::at(int i1, int i2, int i3, int i4)
{
	if (i1 < matrix4<T>::getLowerBound(1) || i1 > matrix4<T>::getUpperBound(1))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I1);
	if (i2 < matrix4<T>::getLowerBound(2) || i2 > matrix4<T>::getUpperBound(2))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I2);
	if (i3 < matrix4<T>::getLowerBound(3) || i3 > matrix4<T>::getUpperBound(3))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I3);
	if (i4 < matrix4<T>::getLowerBound(4) || i4 > matrix4<T>::getUpperBound(4))
		throw std::out_of_range(MESSAGE_OUT_OF_RANGE_I4);
	return iliffeVector[i1][i2][i3][i4];
}

template<typename T>
inline int ilmatrix4<T>::getComplexity()
{
	return matrix4<T>::getLength() * 4;
}

template<typename T>
inline int ilmatrix4<T>::getDimension(int index)
{
	return 0;
}
