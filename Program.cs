using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

const int min_desc_len = 3;
const int max_desc_len = 20;
const int max_items = 5;
const int min_choice = 0;
const int max_choice = 7;
const int min_filename_len = 1;
const int max_filename_len = 10;
const decimal gst_rate = 0.05m;

const int tip_none = 0;
const int tip_percent = 1;
const int tip_amount = 2;

List<string> descriptions = new List<string>();
List<decimal> prices = new List<decimal>();

int tipMethod = tip_none;
decimal tipValue = 0m;
bool tipWasSet = false;

try
{
    bool isRunning = true;

    while (isRunning)
    {
        ShowMenu();
        int choice = ReadChoice();
        isRunning = HandleChoice(choice);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Помилка: " + ex.Message);
}

bool HandleChoice(int choice)
{
    switch (choice)
    {
        case 1:
            AddItem();
            return true;
        case 2:
            RemoveItem();
            return true;
        case 3:
            AddTip();
            return true;
        case 4:
            DisplayBill();
            return true;
        case 5:
            ClearAll();
            return true;
        case 6:
            SaveToFile();
            return true;
        case 7:
            LoadFromFile();
            return true;
        default:
            Console.WriteLine();
            Console.WriteLine("Good-bye and thanks for using this program.");
            return false;
    }
}

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine(" ___________________________");
    Console.WriteLine("|  _________________________ |");
    Console.WriteLine("| |                         ||");
    Console.WriteLine("| | Cat's Cafe              ||");
    Console.WriteLine("| | ----------------------- ||");
    Console.WriteLine("| | 1. Add Item             ||");
    Console.WriteLine("| | 2. Remove Item          ||");
    Console.WriteLine("| | 3. Add Tip              ||");
    Console.WriteLine("| | 4. Display Bill         ||");
    Console.WriteLine("| | 5. Clear All            ||");
    Console.WriteLine("| | 6. Save to file         ||");
    Console.WriteLine("| | 7. Load from file       ||");
    Console.WriteLine("| | 0. Exit                 ||");
    Console.WriteLine("| |_________________________||");
    Console.WriteLine("|____________________________|");
    Console.Write("Enter your choice: ");
}

int ReadChoice()
{
    while (true)
    {
        string input = Console.ReadLine();

        if (TryParseChoice(input, min_choice, max_choice, out int choice))
            return choice;

        Console.WriteLine("Please enter a whole number between 0 and 7.");
        Console.Write("Enter your choice: ");
    }
}

void AddItem()
{
    Console.WriteLine();

    
    if (descriptions.Count >= max_items)
    {
        Console.WriteLine("Order already has the maximum of " + max_items + " items. You can't add more.");
        return;
    }

    string description = ReadDescription();
    decimal price = ReadPrice();

    descriptions.Add(description);
    prices.Add(price);
    Console.WriteLine("Add item was successful.");
}

string ReadDescription()
{
    Console.Write("Enter description: ");
    string input = "";

    while (true)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            if (input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                Console.Write("\b \b");
            }
            continue;
        }

        if (keyInfo.Key == ConsoleKey.Enter)
        {
            if (input.Length >= min_desc_len && input.Length <= max_desc_len)
            {
                Console.WriteLine();
                return input;
            }
            continue;
        }

        char c = keyInfo.KeyChar;

        if (input.Length >= max_desc_len)
            continue;

        if (!char.IsControl(c))
        {
            input = input + c;
            Console.Write(c);
        }
    }
}

decimal ReadPrice()
{
    while (true)
    {
        Console.Write("Enter price: ");
        string input = Console.ReadLine();

        if (TryParsePrice(input, out decimal price))
            return price;

        Console.WriteLine("Price must be a positive number (e.g. 4.50).");
    }
}

void RemoveItem()
{
    Console.WriteLine();

    if (descriptions.Count == 0)
    {
        Console.WriteLine("There are no items in the bill to remove.");
        return;
    }

    PrintNumberedItems();

    while (true)
    {
        Console.Write("Enter the item number to remove or 0 to cancel: ");
        string input = Console.ReadLine();

        if (!TryParseChoice(input, 0, descriptions.Count, out int itemNumber))
        {
            Console.WriteLine("Please enter a whole number between 0 and " + descriptions.Count + ".");
            continue;
        }

        if (itemNumber == 0)
        {
            Console.WriteLine("Remove cancelled.");
            return;
        }

        descriptions.RemoveAt(itemNumber - 1);
        prices.RemoveAt(itemNumber - 1);
        Console.WriteLine("Remove item was successful.");
        return;
    }
}

void AddTip()
{
    Console.WriteLine();

    if (descriptions.Count == 0)
    {
        Console.WriteLine("There are no items in the bill to add tip for.");
        return;
    }

    decimal netTotal = GetNetTotal();

    Console.WriteLine("Net Total: " + FormatMoney(netTotal));
    Console.WriteLine("1 - Tip Percentage");
    Console.WriteLine("2 - Tip Amount");
    Console.WriteLine("3 - No Tip");

    int method = ReadTipMethod();

    switch (method)
    {
        case 1:
            Console.Write("Enter tip percentage: ");
            tipValue = ReadNonNegative();
            tipMethod = tip_percent;
            break;
        case 2:
            Console.Write("Enter Tip amount: ");
            tipValue = ReadNonNegative();
            tipMethod = tip_amount;
            break;
        default:
            tipMethod = tip_none;
            tipValue = 0m;
            break;
    }

    tipWasSet = true;

    Console.WriteLine();
    PrintBillTable();
}

int ReadTipMethod()
{
    while (true)
    {
        Console.Write("Enter Tip Method: ");
        string input = Console.ReadLine();

        if (TryParseChoice(input, 1, 3, out int method))
            return method;

        Console.WriteLine("Please enter 1, 2 or 3.");
    }
}

decimal ReadNonNegative()
{
    while (true)
    {
        string input = Console.ReadLine();

        if (TryParseNonNegative(input, out decimal value))
            return value;

        Console.WriteLine("Value must be a non-negative number.");
    }
}

void DisplayBill()
{
    Console.WriteLine();

    if (descriptions.Count == 0)
    {
        Console.WriteLine("There are no items in the bill to display.");
        return;
    }

    PrintBillTable();
}

void ClearAll()
{
    Console.WriteLine();
    descriptions.Clear();
    prices.Clear();
    tipMethod = tip_none;
    tipValue = 0m;
    tipWasSet = false;
    Console.WriteLine("All items have been cleared.");
}

void SaveToFile()
{
    Console.WriteLine();

    if (descriptions.Count == 0)
    {
        Console.WriteLine("There are no items in the bill to save.");
        return;
    }

    string fileName = ReadFileName("Enter the file path to save items to: ");

    if (TrySaveToFile(fileName, out string errorMessage))
        Console.WriteLine("Write to file " + fileName + " was successful.");
    else
        Console.WriteLine(errorMessage);
}

bool TrySaveToFile(string filePath, out string errorMessage)
{
    try
    {
        using StreamWriter writer = new StreamWriter(filePath, false);

        for (int i = 0; i < descriptions.Count; i++)
        {
            string safeDescription = descriptions[i].Replace(",", " ");
            writer.WriteLine(safeDescription + "," + prices[i].ToString(CultureInfo.InvariantCulture));
        }

        errorMessage = "";
        return true;
    }
    catch (Exception ex)
    {
        errorMessage = "Could not save file: " + ex.Message;
        return false;
    }
}

void LoadFromFile()
{
    Console.WriteLine();

    string fileName = ReadFileName("Enter the file path to load items from: ");

    if (TryLoadFromFile(fileName, out string errorMessage))
        Console.WriteLine("Read from " + fileName + " was successful.");
    else
        Console.WriteLine(errorMessage);
}

bool TryLoadFromFile(string filePath, out string errorMessage)
{
    try
    {
        if (!File.Exists(filePath))
        {
            errorMessage = "File not found.";
            return false;
        }

        List<string> loadedDescriptions = new List<string>();
        List<decimal> loadedPrices = new List<decimal>();

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
                continue;

            int commaIndex = line.LastIndexOf(',');
            if (commaIndex <= 0 || commaIndex == line.Length - 1)
                continue;

            string description = line.Substring(0, commaIndex).Trim();
            string priceText = line.Substring(commaIndex + 1).Trim();

            bool priceOk = decimal.TryParse(priceText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out decimal price);

            if (priceOk && IsValidDescription(description) && price > 0 && loadedDescriptions.Count < max_items)
            {
                loadedDescriptions.Add(description);
                loadedPrices.Add(price);
            }
        }

        descriptions = loadedDescriptions;
        prices = loadedPrices;

        errorMessage = "";
        return true;
    }
    catch (Exception ex)
    {
        errorMessage = "Could not load file: " + ex.Message;
        return false;
    }
}

string ReadFileName(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        if (IsValidFileName(input))
            return input.Trim();

        Console.WriteLine("File name must be between " + min_filename_len + " and " + max_filename_len + " characters.");
    }
}

bool IsValidDescription(string description)
{
    if (description == null)
        return false;

    int len = description.Trim().Length;
    return len >= min_desc_len && len <= max_desc_len;
}

bool TryParsePrice(string text, out decimal price)
{
    bool ok = decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out price);
    return ok && price > 0;
}

bool TryParseNonNegative(string text, out decimal value)
{
    bool ok = decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out value);
    return ok && value >= 0;
}

bool TryParseChoice(string text, int minValue, int maxValue, out int choice)
{
    bool ok = int.TryParse(text, out choice);
    return ok && choice >= minValue && choice <= maxValue;
}

bool IsValidFileName(string fileName)
{
    if (string.IsNullOrWhiteSpace(fileName))
        return false;

    string baseName = Path.GetFileNameWithoutExtension(fileName.Trim());
    return baseName.Length >= min_filename_len && baseName.Length <= max_filename_len;
}

decimal GetNetTotal()
{
    decimal netTotal = 0m;

    for (int i = 0; i < prices.Count; i++)
        netTotal += prices[i];

    return netTotal;
}

decimal GetTipAmount(decimal netTotal)
{
    if (tipMethod == tip_percent)
        return Math.Round(netTotal * tipValue / 100m, 2);

    if (tipMethod == tip_amount)
        return Math.Round(tipValue, 2);

    return 0m;
}

decimal GetGstAmount(decimal netTotal)
{
    return Math.Round(netTotal * gst_rate, 2);
}

void PrintNumberedItems()
{
    Console.WriteLine();

    int noWidth = 7;
    int descWidth = 20;
    int priceWidth = 10;

    string noHeader = "ItemNo";
    string descHeader = "Description";
    string priceHeader = "Price";
    Console.WriteLine(noHeader + Spaces(noWidth - noHeader.Length) + descHeader + Spaces(descWidth - descHeader.Length) + Spaces(priceWidth - priceHeader.Length) + priceHeader);

    string noDashes = "------";
    string descDashes = "-----------";
    string priceDashes = "------";
    Console.WriteLine(noDashes + Spaces(noWidth - noDashes.Length) + descDashes + Spaces(descWidth - descDashes.Length) + Spaces(priceWidth - priceDashes.Length) + priceDashes);

    for (int i = 0; i < descriptions.Count; i++)
    {
        string itemNoText = (i + 1).ToString();
        string priceText = FormatMoney(prices[i]);

        Console.WriteLine(itemNoText + Spaces(noWidth - itemNoText.Length) + descriptions[i] + Spaces(descWidth - descriptions[i].Length) + Spaces(priceWidth - priceText.Length) + priceText);
    }
}
void PrintBillTable()
{
    decimal netTotal = GetNetTotal();
    decimal tipAmount = GetTipAmount(netTotal);
    decimal gstAmount = GetGstAmount(netTotal);
    decimal totalAmount = netTotal + tipAmount + gstAmount;

    int descWidth = 20;
    int priceWidth = 10;

    string priceHeaderText = "Price";
    Console.WriteLine("Description" + Spaces(descWidth - "Description".Length) + Spaces(priceWidth - priceHeaderText.Length) + priceHeaderText);

    string dashes = "";
    for (int d = 0; d < descWidth + priceWidth; d++)
        dashes = dashes + "-";
    Console.WriteLine(dashes);

    for (int i = 0; i < descriptions.Count; i++)
    {
        string priceText = FormatMoney(prices[i]);
        Console.WriteLine(descriptions[i] + Spaces(descWidth - descriptions[i].Length) + Spaces(priceWidth - priceText.Length) + priceText);
    }

    Console.WriteLine(dashes);

    string netTotalText = FormatMoney(netTotal);
    Console.WriteLine(Spaces(descWidth - "Net Total".Length) + "Net Total" + Spaces(priceWidth - netTotalText.Length) + netTotalText);

    string tipAmountText = FormatMoney(tipAmount);
    Console.WriteLine(Spaces(descWidth - "Tip Amount".Length) + "Tip Amount" + Spaces(priceWidth - tipAmountText.Length) + tipAmountText);

    string gstLabel = tipWasSet ? "GST Amount" : "Total GST";
    string gstAmountText = FormatMoney(gstAmount);
    Console.WriteLine(Spaces(descWidth - gstLabel.Length) + gstLabel + Spaces(priceWidth - gstAmountText.Length) + gstAmountText);

    string totalAmountText = FormatMoney(totalAmount);
    Console.WriteLine(Spaces(descWidth - "Total Amount".Length) + "Total Amount" + Spaces(priceWidth - totalAmountText.Length) + totalAmountText);
}

string Spaces(int count)
{
    string result = "";

    for (int i = 0; i < count; i++)
        result = result + " ";

    return result;
}

string FormatMoney(decimal amount)
{
    return amount.ToString("$0.00");
}
