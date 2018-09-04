using System;
using System.Collections;
using System.Collections.Generic;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет строго типизированное бинарное дерево поиска.
	/// </summary>
	/// <typeparam name="T">Тип элементов в бинарном дереве поиска.</typeparam>
	public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable
	{

		#region ~Fields~

		BinaryNode _root;

		#endregion

		#region ~Constructors~

		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="BinarySearchTree{T}"/>, который является пустым.
		/// </summary>
		public BinarySearchTree()
		{
			Count = 0;
		}

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="BinarySearchTree{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Последовательность, элементы которой копируются в двоичное дерево поиска.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public BinarySearchTree(IEnumerable<T> collection) : this()
		{
			foreach (var item in collection)
				Add(item);
		}

		#endregion

		#region ~Properties~

		public int Count { get; private set; }

		#endregion

		#region ~Methods~

		/// <summary>
		/// Добавляет объект в указанное поддерево двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="node">Поддерево, в которое будет добавлен элемент.</param>
		/// <param name="item">Объект, добавляемый в двоичное дерево поиска <see cref="BinarySearchTree{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		private void Add(BinaryNode node, T item)
		{
			var itemNode = new BinaryNode(item);
			var current = _root;
			while (current != null)
				switch (item.CompareTo(current.Value))
				{
					case -1:
						if (current.Left == null)
						{
							current.Left = itemNode;
							Count++;
							return;
						}
						else
							current = current.Left;
						break;
					case 0:
						throw new ArgumentException("Элемент с указанным ключём уже существует!", nameof(item));
					case 1:
						if (current.Right == null)
						{
							current.Right = itemNode;
							Count++;
							return;
						}
						else
							current = current.Right;
						break;
					default:
						throw new Exception("Если вы видите эту ошибку, срочно напишите об этом на адрес kozakovi4@yandex.ru!");
				}
			_root = itemNode;
			Count++;
		}

		/// <summary>
		/// Добавляет объект в двоичное дерево поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в двоичное дерево поиска <see cref="BinarySearchTree{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public void Add(T item) => Add(_root, item);
		
		/// <summary>
		/// Удаляет первое вхождение указанного объекта из двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="item">Объект, который необходимо удалить из двоичного дерева поиска <see cref="BinarySearchTree{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <returns>Значение <see cref="true"/>, если элемент <paramref name="item"/> успешно удален, в противном случае — значение <see cref="false"/>. Этот метод также возвращает <see cref="false"/>, если элемент <paramref name="item"/> не найден в односвязном списке <see cref="BinarySearchTree{T}"/>.</returns>
		public bool Remove(T item)
		{
			BinaryNode current = _root, parent = null;
			bool left = false;
			if (current == null)
				return false;
			if (item.CompareTo(current.Value) == 0)
			{
				_root = null;
				Count--;
			}
			else
				while (current != null)
				{
					switch (item.CompareTo(current.Value))
					{
						case -1:
							left = true;
							parent = current;
							current = current.Left;
							break;
						case 0:
							if (left)
								parent.Left = null;
							else
								parent.Right = null;
							using (var enumerator = GetPreorderEnumerator(current))
							{
								enumerator.MoveNext();
								while(enumerator.MoveNext())
									Add(parent, enumerator.Current);
							}
							Count--;
							return true;
						case 1:
							left = false;
							parent = current;
							current = current.Right;
							break;
					}
				}
			return false;
		}

		/// <summary>
		/// Выполняет прямой обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Последовательность, в которой обрабатываются корни поддеревьев.</returns>
		public IEnumerable<T> PreorderTraverse()
		{
			var result = new List<T>(Count);
			using (var enumerator = GetPreorderEnumerator())
				while (enumerator.MoveNext())
					result.Add(enumerator.Current);
			return result;
		}

		/// <summary>
		/// Выполняет прямой обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="action">Действие, которое будет выполнено над корнями поддеревьев.</param>
		public void PreorderTraverse(Action<T> action)
		{
			using (var enumerator = GetPreorderEnumerator())
				while (enumerator.MoveNext())
					action(enumerator.Current);
		}

		/// <summary>
		/// Выполняет симметричный обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Последовательность, в которой обрабатываются корни поддеревьев.</returns>
		public IEnumerable<T> InorderTraverse()
		{
			var result = new List<T>(Count);
			using (var enumerator = GetInorderEnumerator())
				while (enumerator.MoveNext())
					result.Add(enumerator.Current);
			return result;
		}

		/// <summary>
		/// Выполняет симметричный обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="action">Действие, которое будет выполнено над корнями поддеревьев.</param>
		public void InorderTraverse(Action<T> action)
		{
			using (var enumerator = GetInorderEnumerator())
				while (enumerator.MoveNext())
					action(enumerator.Current);
		}

		/// <summary>
		/// Выполняет обратный обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Последовательность, в которой обрабатываются корни поддеревьев.</returns>

		public IEnumerable<T> PostorderTraverse()
		{
			var result = new List<T>(Count);
			using (var enumerator = GetPostorderEnumerator())
				while (enumerator.MoveNext())
					result.Add(enumerator.Current);
			return result;
		}

		/// <summary>
		/// Выполняет обратный обход двоичного дерева <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <param name="action">Действие, которое будет выполнено над корнями поддеревьев.</param>

		public void PostorderTraverse(Action<T> action)
		{
			using (var enumerator = GetPostorderEnumerator())
				while (enumerator.MoveNext())
					action(enumerator.Current);
		}

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при симметричном обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		public IEnumerator<T> GetEnumerator() => GetInorderEnumerator();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при симметричном обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetInorderEnumerator();

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="BinaryEnumerator"/> для <see cref="BinarySearchTree{T}"/>.</returns>

		public BinaryEnumerator GetBinaryEnumerator() => new BinaryEnumerator(this);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при прямом обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>, начиная с указанного узла.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		private IEnumerator<T> GetPreorderEnumerator(BinaryNode node)
		{
			Stack<BinaryNode> stack = new Stack<BinaryNode>();
			BinaryNode current = node;
			while (stack.Count > 0 || current != null)
				if (current != null)
				{
					yield return current.Value;
					stack.Push(current);
					current = current.Left;
				}
				else
				{
					current = stack.Pop();
					current = current.Right;
				}
		}

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при прямом обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		public IEnumerator<T> GetPreorderEnumerator() => GetPreorderEnumerator(_root);

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при симметричном обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		public IEnumerator<T> GetInorderEnumerator()
		{
			Stack<BinaryNode> stack = new Stack<BinaryNode>();
			BinaryNode current = _root;
			while (stack.Count > 0 || current != null)
				if (current != null)
				{
					stack.Push(current);
					current = current.Left;
				}
				else
				{
					current = stack.Pop();
					yield return current.Value;
					current = current.Right;
				}
		}

		/// <summary>
		/// Возвращает перечислитель, осуществляющий перебор элементов при обратном обходе двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		/// <returns>Новый объект <see cref="IEnumerator{T}"/> для <see cref="BinarySearchTree{T}"/>.</returns>
		public IEnumerator<T> GetPostorderEnumerator()
		{
			Stack<BinaryNode> stack = new Stack<BinaryNode>();
			BinaryNode current = _root;
			BinaryNode last = null;
			while (stack.Count > 0 || current != null)
				if (current != null)
				{
					stack.Push(current);
					current = current.Left;
				}
				else
				{
					var peeked = stack.Peek();
					if (peeked.Right != null && last != peeked.Right)
						current = peeked.Right;
					else
					{
						stack.Pop();
						yield return peeked.Value;
						last = peeked;
					}
				}
		}

		#endregion

		#region ~Classes~

		/// <summary>
		/// Представляет собой узел двоичного дерева.
		/// </summary>
		/// <typeparam name="T">Тип данных, хранящихся в <see cref="Value"/>.</typeparam>
		private class BinaryNode
		{
			/// <summary>
			/// Инициализирует новый класс <see cref="BinaryNode<T>"/> со значением <paramref name="value"/>.
			/// </summary>
			/// <param name="value"></param>
			public BinaryNode(T value)
			{
				Value = value;
			}

			/// <summary>
			/// Корень левого двоичного поддерева.
			/// </summary>
			public BinaryNode Left { get; set; }

			/// <summary>
			/// Корень правого двоичного поддерева.
			/// </summary>
			public BinaryNode Right { get; set; }

			/// <summary>
			/// Данные, хранящиеся в текущем узле.
			/// </summary>
			public T Value { get; set; }

		}

		/// <summary>
		/// Выполняет перечисление элементов двоичного дерева поиска <see cref="BinarySearchTree{T}"/>.
		/// </summary>
		public struct BinaryEnumerator : IDisposable
		{

			#region ~Fields~

			// Двоичное дерево.
			BinarySearchTree<T> _tree;

			// Корневой узел текущего двоичного поддерева.
			private BinaryNode _currentNode;

			// Корневой узел левого двоичного поддерева.
			private BinaryNode _leftNode;

			// Корневой узел правого двоичного поддерева.
			private BinaryNode _rightNode;

			#endregion

			#region ~Properties~

			/// <summary>
			/// Возвращает элемент, расположенный в текущей позиции перечислителя.
			/// </summary>
			/// <value>Элемент односвязного списка <see cref="SinglyLinkedList{T}"/>, находящийся в текущей позиции перечислителя.</value>
			/// <exception cref="InvalidOperationException">Перечеслитель находится в начальном положении, т. е. перед первым элементом коллекции.</exception>
			public T Current
			{
				get
				{
					if (_currentNode == null)
						throw new InvalidOperationException();
					return _currentNode.Value;
				}
			}

			#endregion

			#region ~Constructors~

			internal BinaryEnumerator(BinarySearchTree<T> tree)
			{
				_tree = tree;
				_currentNode = null;
				_leftNode = _rightNode = _tree._root;
			}

			#endregion

			#region ~Methods~

			/// <summary>
			/// Перемещает перечислитель к левому узлу двоичного дерева <see cref="BinarySearchTree{T}"/>.
			/// </summary>
			/// <returns>Значение <see cref="true"/>, если перечислитель был успешно перемещен к левому узлу; значение <see cref="false"/>, если перечислитель достиг конца поддерева.</returns>
			public bool MoveLeft()
			{
				_currentNode = _leftNode;
				if (_currentNode == null)
					return false;
				_leftNode = _currentNode.Left;
				_rightNode = _currentNode.Right;
				return true;
			}

			/// <summary>
			/// Перемещает перечислитель к правому узлу двоичного дерева <see cref="BinarySearchTree{T}"/>.
			/// </summary>
			/// <returns>Значение <see cref="true"/>, если перечислитель был успешно перемещен к правому узлу; значение <see cref="false"/>, если перечислитель достиг конца поддерева.</returns>
			public bool MoveRight()
			{
				_currentNode = _rightNode;
				if (_currentNode == null)
					return false;
				_leftNode = _currentNode.Left;
				_rightNode = _currentNode.Right;
				return true;
			}

			/// <summary>
			/// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом односвязного списка.
			/// </summary>
			public void Reset()
			{
				_currentNode = null;
				_leftNode = _rightNode = _tree._root;
			}

			/// <summary>
			/// Освобождает все ресурсы, занятые модулем <see cref="BinaryEnumerator"/>.
			/// </summary>
			public void Dispose()
			{
				_tree = null;
				_currentNode = _leftNode = _rightNode = null;
			}
			
			#endregion

		}

		#endregion

	}
}
