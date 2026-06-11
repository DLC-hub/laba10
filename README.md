Лабораторная работа 10.



Тема работы: Dependency Injection и паттерн «Сервис» в MVVM-приложениях

Цель работы: изучить принцип внедрения зависимостей (Dependency Injection, DI) и паттерн «Сервис» для решения проблемы жёсткой связности компонентов в MVVM-приложениях.


- Dependency Injection (DI) — это метод передачи объекту необходимых ему зависимостей (например, сервисов) снаружи, а не их самостоятельное создание внутри. Это обеспечивает гибкость кода, изоляцию для модульного тестирования и упрощает поддержку приложений, следующих шаблону MVVM


Dependency Injection (Внедрение зависимостей)В традиционном подходе класс (например, ViewModel) создает нужный ему сервис напрямую с помощью оператора new. Из-за этого классы становятся жестко связанными.При использовании DI компоненты зависят от абстракций (интерфейсов), а сам процесс внедрения состоит из трех основных шагов:Создание контракта: Создается интерфейс, описывающий функционал (например, IDataService).Реализация: Пишется класс, выполняющий задачу (например, ApiService).Передача в класс: Сервис передается в ViewModel через конструктор

-----------------------------------------------------------

1. App.xaml.cs
<img width="675" height="466" alt="image" src="https://github.com/user-attachments/assets/e3e7d305-ebab-4c32-a6cf-307a5aff3ed5" />



-----------------------------------------------------------


2.IDialogServicenew.cs
<img width="920" height="540" alt="image" src="https://github.com/user-attachments/assets/f154ec9d-c947-4193-b995-9a060553a30d" />


-----------------------------------------------------------

3.IDialogService.cs


<img width="722" height="348" alt="image" src="https://github.com/user-attachments/assets/ec82c3e4-7b9c-4949-8849-a11ece359453" />


-----------------------------------------------------------

4.MainWindow.xaml.cs


<img width="328" height="215" alt="image" src="https://github.com/user-attachments/assets/7b4c91c8-68a8-4fe0-80be-c853795bdf91" />

-----------------------------------------------------------

5.RelayCommand.cs


<img width="590" height="558" alt="image" src="https://github.com/user-attachments/assets/4b76dbba-4cb6-48e8-8cd2-24ae316b33b6" />

-----------------------------------------------------------

6.MainViewModel.cs

(нормально скрин не сделать, код не уместился, поэтому прилагаю его полностью)

using DocumentFormat.OpenXml.Office2010.Excel;

using Nest;

using PhoneBookApp.Commands;

using PhoneBookApp.Models;

using PhoneBookApp.Services;

using System;

using System.Collections.Generic;

using System.Collections.ObjectModel;

using System.ComponentModel;

using System.Linq;

using System.Runtime.CompilerServices;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Input;

namespace PhoneBookApp.ViewModels

{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDialogService _dialogService;
        private ObservableCollection<Contact> _contacts;
        private Contact _selectedContact;
        private string _newContactName;
        private string _newContactPhone;
        private int _nextId = 1;

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                // Это карчое новая команда удаления при изменении выбора
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        // войства для ввода нового контакта
        public string NewContactName
        {
            get => _newContactName;
            set
            {
                _newContactName = value;
                OnPropertyChanged();
            }
        }

        public string NewContactPhone
        {
            get => _newContactPhone;
            set
            {
                _newContactPhone = value;
                OnPropertyChanged();
            }
        }

        // Это всё команды
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            Contacts = new ObservableCollection<Contact>();

            // тестовые данные для демонстрации
            AddTestData();

            AddCommand = new RelayCommand(_ => AddContact(), _ => CanAddContact());
            DeleteCommand = new RelayCommand(_ => DeleteContact(), _ => SelectedContact != null);
        }

        private void AddTestData()
        {
            Contacts.Add(new Contact { Id = _nextId++, Name = "Иван Петров", Phone = "+79161234567" });
            Contacts.Add(new Contact { Id = _nextId++, Name = "Мария Сидорова", Phone = "+79169876543" });
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(NewContactName) && !string.IsNullOrWhiteSpace(NewContactPhone);
        }

        private void AddContact()
        {
            // Проверка дубликата по номеру телефона
            if (Contacts.Any(c => c.Phone == NewContactPhone))
            {
                _dialogService.ShowWarning($"Контакт с номером {NewContactPhone} уже существует.\nДобавление отменено.");
                return;
            }

            // Новый контакт
            var newContact = new Contact
            {
                Id = _nextId++,
                Name = NewContactName.Trim(),
                Phone = NewContactPhone.Trim()
            };
            Contacts.Add(newContact);

            // Очистка полей ввода
            NewContactName = string.Empty;
            NewContactPhone = string.Empty;

            _dialogService.ShowInfo($"Контакт \"{newContact.Name}\" успешно добавлен.");
        }

        private void DeleteContact()
        {
            if (SelectedContact == null) return;

            // Запрос подтверждения удаления
            if (_dialogService.ShowConfirmation($"Вы действительно хотите удалить контакт \"{SelectedContact.Name}\"?"))
            {
                Contacts.Remove(SelectedContact);
                _dialogService.ShowInfo("Контакт удалён.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}







7. Contact.cs

   
<img width="588" height="668" alt="image" src="https://github.com/user-attachments/assets/b76dac57-7169-4af6-afb5-c72dd5bb96b9" />


8.MainWindow.xaml


<img width="670" height="519" alt="image" src="https://github.com/user-attachments/assets/55b7b427-b9fc-4090-8966-be6d5d963db5" />

<img width="1032" height="529" alt="image" src="https://github.com/user-attachments/assets/92c3e8ec-ec79-4706-a8ea-52ba3cdc24f5" />

