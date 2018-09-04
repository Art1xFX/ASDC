#pragma once
#include "_matrix4.h"
#include "resource.h"

template<typename T>
/// <summary>
/// Представляет строго типизированный четырёхмерный массив объектов, доступных по индексу.
/// </summary>
class matrix4 : public _matrix4<T>
{
	int** index;
	size_t* length;
protected:
	T* _vector;

public:
	/// <summary>
	/// Инициализирует новый пустой экземпляр четырёхмерного массива <see cref="matrix4"/> по заданным интервалам измерений.
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
	matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h);

	/// <summary>
	/// Инициализирует новый экземпляр четырёхмерного массива <see cref="matrix4"/> по заданным интервалам измерений, который содержит элементы, скопированные из указанного массива.
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
	matrix4(int i1l, int i1h, int i2l, int i2h, int i3l, int i3h, int i4l, int i4h, T* array, size_t length);

	/// <summary>
	/// Освобождает все ресурсы, занятые <see cref="matrix4"/>.
	/// </summary>
	~matrix4();

	/// <summary>
	/// Возвращает или задает элемент по указанным индексам.
	/// </summary>
	/// <param name='i1'>Первый индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i2'>Второй индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i3'>Третий индекс элемента, который необходимо получить или задать.</param>
	/// <param name='i4'>Четвёртый индекс элемента, который необходимо получить или задать.</param>
	/// <returns>Ссылка на элемент, расположенный по указанным индексам.</returns>
	/// <exception cref="std::out_of_range">Значение индексов находятся за границами допустимого диапазона <see cref="getLowerBound"/> и <see cref="getUpperBound"/>.</exception>
	virtual T& at(int i1, int i2, int i3, int i4) = 0;

	/// <summary>
	/// Получает общее число элементов во всех измерениях массива <see cref="matrix4"/>.
	/// </summary>
	size_t getLength();

	/// <summary>
	/// Возвращает число, представляющее количество элементов в заданном измерении массива <see cref="matrix4"/>.
	/// </summary>
	/// <param name='dimension'>Измерение массива <see cref="matrix4"/>, индексация которого начинается с единицы, для которого требуется определить длину.</param>
	/// <returns>32-битовое целое число без знака, представляющее количество элементов в заданном измерении.</returns>
	/// <exception cref="std::out_of_range">
	/// Значение параметра <paramref name="dimension"/> меньше нуля.
	/// - или -
	/// Значение параметра <paramref name="dimension"/> больше четырёх.
	/// </exception>
	int getLength(int dimension);


	/// <summary>
	/// Возвращает число, представляющее количество операций сложения при вычислении адреса элемента.
	/// </summary>
	/// <returns>32-битовое целое число без знака, представляющее количество операций сложения при вычислении адреса элемента.</returns>
	virtual int getAddCount() = 0;

	/// <summary>
	/// Возвращает число, представляющее количество операций умножения при вычислении адреса элемента.
	/// </summary>
	/// <returns>32-битовое целое число без знака, представляющее количество операций умножения при вычислении адреса элемента.</returns>
	virtual int getMulCount() = 0;

	/// <summary>
	/// Получает индекс первого элемента заданного измерения в массиве.
	/// </summary>
	/// <param name='dimension'>Измерение массива, индексация которого начинается с единицы, для которого необходимо определить нижнюю границу.</param>
	/// <returns>Индекс первого элемента заданного измерения в массиве.</returns>
	/// <exception cref="std::out_of_range">
	/// Значение параметра <paramref name="dimension"/> меньше нуля.
	/// - или -
	/// Значение параметра <paramref name="dimension"/> больше четырёх.
	/// </exception>
	int getLowerBound(int dimension);

	/// <summary>
	/// Получает индекс последнего элемента заданного измерения в массиве.
	/// </summary>
	/// <param name='dimension'>Измерение массива, индексация которого начинается с единицы, для которого необходимо определить верхнюю границу.</param>
	/// <returns>Индекс последнего элемента указанного измерения в массиве.</returns>
	/// <exception cref="std::out_of_range">
	/// Значение параметра <paramref name="dimension"/> меньше нуля.
	/// - или -
	/// Значение параметра <paramref name="dimension"/> больше четырёх.
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



