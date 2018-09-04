#pragma once
#include "matrix4.h"

template<typename T>
/// <summary>
/// Представляет строго типизированный четырёхмерный массив объектов, расположенных по строкам, доступных по индексу, использующий вектор Айлиффа.
/// </summary>
class ilmatrix4 : public matrix4<T>
{
	T**** iliffeVector;

public:
	/// <summary>
	/// Инициализирует новый пустой экземпляр четырёхмерного массива <see cref="icmatrix4"/> по заданным интервалам измерений.
	/// </summary>
	/// <param name='i1l'>Нижняя граница первого измерения создаваемого массива.</param>
	/// <param name='i1h'>Верхняя граница первого измерения создаваемого массива</param>
	/// <param name='i2l'>Нижняя граница второго измерения создаваемого массива.</param>
	/// <param name='i2h'>Верхняя граница второго измерения создаваемого массива.</param>
	/// <param name='i3l'>Нижняя граница третьего измерения создаваемого массива.</param>
	/// <param name='i3h'>Верхняя граница третьего измерения создаваемого массива.</param>
	/// <param name='i4l'>Нижняя граница четвёртого измерения создаваемого массива.</param>
	/// <param name='i4h'>Верхняя граница четвёртого измерения создаваемого массива.</param>
	/// <exception cref="std::invalid_argument">
	/// Значение параметра <paramref name="i1h"/> меньше <paramref name="i1l"/>.
	/// -или -
	///	Значение параметра <paramref name="i2h"/> меньше <paramref name="i2l"/>.
	/// -или -
	///	Значение параметра <paramref name="i3h"/> меньше <paramref name="i3l"/>.
	/// -или -
	///	Значение параметра <paramref name="i4h"/> меньше <paramref name="i4l"/>.
	/// </exception>
	ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h);

	/// <summary>
	/// Инициализирует новый экземпляр четырёхмерного массива <see cref="icmatrix4"/> по заданным интервалам измерений, который содержит элементы, скопированные из указанного массива.
	/// </summary>	
	/// <param name='i1l'>Нижняя граница первого измерения создаваемого массива.</param>
	/// <param name='i1h'>Верхняя граница первого измерения создаваемого массива</param>
	/// <param name='i2l'>Нижняя граница второго измерения создаваемого массива.</param>
	/// <param name='i2h'>Верхняя граница второго измерения создаваемого массива.</param>
	/// <param name='i3l'>Нижняя граница третьего измерения создаваемого массива.</param>
	/// <param name='i3h'>Верхняя граница третьего измерения создаваемого массива.</param>
	/// <param name='i4l'>Нижняя граница четвёртого измерения создаваемого массива.</param>
	/// <param name='i4h'>Верхняя граница четвёртого измерения создаваемого массива.</param>
	/// <param name='array'>Массив, элементы которого копируются в новый четырёхмерный массив.</param>
	/// <param name="length">Количество элементов в массиве <paramref name="array"/></param>
	/// <exception cref="std::invalid_argument">
	/// Значение параметра <paramref name="i1h"/> меньше <paramref name="i1l"/>.
	/// -или -
	///	Значение параметра <paramref name="i2h"/> меньше <paramref name="i2l"/>.
	/// -или -
	///	Значение параметра <paramref name="i3h"/> меньше <paramref name="i3l"/>.
	/// -или -
	///	Значение параметра <paramref name="i4h"/> меньше <paramref name="i4l"/>.
	/// -или -
	/// Значение параметра <paramref name="array"/> равно nullptr.
	/// -или -
	/// Значение параметра <paramref name="length"/> меньше нуля.
	/// </exception>
	ilmatrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T* array, size_t length);

	/// <summary>
	/// Освобождает все ресурсы, занятые <see cref="icmatrix4"/>.
	/// </summary>
	~ilmatrix4();

	/// <summary>
	/// Возвращает число, представляющее количество операций сложения при вычислении адреса элемента.
	/// </summary>
	/// <returns>32-битовое целое число без знака, представляющее количество операций сложения при вычислении адреса элемента.</returns>
	int getAddCount();

	/// <summary>
	/// Возвращает число, представляющее количество операций умножения при вычислении адреса элемента.
	/// </summary>
	/// <returns>32-битовое целое число без знака, представляющее количество операций умножения при вычислении адреса элемента.</returns>
	int getMulCount();

	/// <summary>
	/// Возвращает или задает элемент по указанным индексам.
	/// </summary>
	/// <param name='i1'>Первый индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i2'>Второй индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i3'>Третий индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i4'>Четвёртый индекс элемента, который необходимо получить или задать.</param>
	/// <returns>Ссылка на элемент, расположенный по указанным индексам.</returns>
	/// <exception cref="std::out_of_range">Значение индексов находятся за границами допустимого диапазона <see cref="getLowerBound"/> и <see cref="getUpperBound"/>.</exception>
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
