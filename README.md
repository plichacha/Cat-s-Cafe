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

**Nesteruk Iryna**

Group: **611p**

---

## License

This project was created for educational purposes.
