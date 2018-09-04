using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ASDC.Collections
{
	/// <summary>
	/// Представляет двустороннюю очередь объектов, основанную на принципе "первым поступил — первым обслужен (FIFO)", которая выдает уведомления при добавлении и удалении элементов, а также при обновлении очереди.
	/// </summary>
	/// <typeparam name="T">Тип элементов в двусторонней очереди.</typeparam>
	public class ObservableDeque<T> : Deque<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{

		#region ~Constructors~
		/// <summary>
		/// Инициализирует новый экземпляр класса <see cref="ObservableDeque{T}"/>, который является пустым.
		/// </summary>
		public ObservableDeque() : base() { }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ObservableDeque{T}"/>, который содержит элементы, скопированные из указанной коллекции.
		/// </summary>
		/// <param name="collection">Коллекция, элементы которой копируются в двустороннюю очередь.</param>
		/// <exception cref="ArgumentNullException">Свойство <paramref name="collection"/> имеет значение <see cref="null"/>.</exception>
		public ObservableDeque(IEnumerable<T> collection) : base(collection) { }

		#endregion

		#region ~Properties~

		/// <summary>
		/// Добавляет объект в конец очереди <see cref="ObservableDeque{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в конец очереди <see cref="ObservableDeque{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public new void PushBack(T item)
		{
			base.PushBack(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		/// <summary>
		/// Добавляет объект в начало очереди <see cref="ObservableDeque{T}"/>.
		/// </summary>
		/// <param name="item">Объект, добавляемый в начало очереди <see cref="ObservableDeque{T}"/>. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public new void PushFront(T item)
		{
			base.PushFront(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, 0));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		/// <summary>
		/// Удаляет объект из конца очереди <see cref="ObservableDeque{T}"/> и возвращает его.
		/// </summary>
		/// <returns>Объект, удаляемый из конца очереди <see cref="ObservableDeque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="ObservableDeque{T}"/> является пустой.</exception>
		public new T PopBack()
		{
			var result = base.PopBack();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, result, Count));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			return result;
		}

		/// <summary>
		/// Удаляет объект из начала очереди <see cref="ObservableDeque{T}"/> и возвращает его.
		/// </summary>
		/// <returns>Объект, удаляемый из начала очереди <see cref="ObservableDeque{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Очередь <see cref="ObservableDeque{T}"/> является пустой.</exception>
		public new T PopFront()
		{
			var result = base.PopFront();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, result, 0));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			return result;
		}

		/// <summary>
		/// Удаляет все элементы двусторонней очереди <see cref="ObservableDeque{T}"/>.
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		#endregion

		#region ~Events~

		/// <summary>
		/// Событие происходит при изменении очереди.
		/// </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Событие происходит при изменении значения свойства.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
