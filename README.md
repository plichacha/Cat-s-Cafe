<<<<<<< HEAD
# Cat's Cafe - консольний застосунок

Один файл `Program.cs` - уся програма, без окремих бібліотек чи проєктів.

## Функціонал
1. Додати страву (опис 3-20 символів, ціна > 0)
2. Видалити страву за номером
3. Додати чайові: відсоток / фіксована сума / без чайових
4. Показати рахунок (Net Total, Tip Amount, GST Amount, Total Amount; GST = 5%)
5. Очистити замовлення
6. Зберегти замовлення у файл (CSV: `Опис,Ціна`)
7. Завантажити замовлення з файлу
0. Вихід

Максимум 5 страв у замовленні - після досягнення ліміту пункт "Add Item" одразу
повідомляє про це і не питає ні опис, ні ціну.

## Запуск
```bash
dotnet run
```
Потрібен .NET SDK 8.0.
=======
# Cat's Cafe Billing System

Console application written in C# for creating and managing café bills.

## Description

This project is a console-based billing system developed in C#. It allows users to create customer bills, manage ordered items, calculate GST and tips, display the final bill, and save or load bills from a file.

The application demonstrates the use of:

- Collections (`List<T>`)
- Input validation
- File handling
- Exception handling
- Methods and modular programming
- Decimal calculations for financial accuracy

---

## Features

- Add items to a bill
- Remove items
- Add tip (percentage or fixed amount)
- Calculate GST (5%)
- Display formatted bill
- Save bill to a file
- Load bill from a file
- Clear current bill
- Input validation
- Error handling

---

## Technologies

- C#
- .NET Console Application
- Visual Studio

---

## Project Structure

```
Program.cs
README.md
```

---

## How to Run

1. Clone the repository

```bash
git clone https://github.com/USERNAME/REPOSITORY.git
```

2. Open the project in Visual Studio.

3. Build the project.

4. Run the application.

---

## Menu

```
1. Add Item
2. Remove Item
3. Add Tip
4. Display Bill
5. Clear All
6. Save to File
7. Load from File
0. Exit
```

---

## Validation Rules

| Item | Rule |
|------|------|
| Description | 3–20 characters |
| Price | Must be greater than 0 |
| Maximum items | 5 |
| Filename | 1–10 characters |

---

## Learning Objectives

This project demonstrates:

- Console application development
- Modular programming
- Data validation
- File I/O
- Exception handling
- Collections
- Financial calculations using `decimal`

---

## Author

**Нестерук Ірина**

Group: **611П**

---

## License

This project was created for educational purposes.
>>>>>>> 64b4bbf65dadeed5ec973a989fff0a5799fde26f
