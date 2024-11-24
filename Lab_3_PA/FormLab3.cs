namespace Lab_3_PA;

public partial class FormLab3 : Form
{
    private readonly RedBlackTree<int, string> _tree;
    private const string FileName = "tree_data.dat"; 

    public FormLab3()
    {
        InitializeComponent();
        _tree = RedBlackTree<int, string>.LoadFromFile(FileName); 
        DisplayTree();
    }

    private void btnAddRecord_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtKey.Text, out int key) && !string.IsNullOrWhiteSpace(txtData.Text))
        {
            try
            {
                _tree.Add(key, txtData.Text);
                MessageBox.Show("Record added successfully.");
                DisplayTree();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Invalid input. Please enter a valid key and value.");
        }
    }

    private void btnSearchByKey_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtKey.Text, out int key))
        {
            try
            {
                string value = _tree.Search(key, out int comparisons);
                MessageBox.Show($"Value: {value}\nComparisons: {comparisons}");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Invalid key.");
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtKey.Text, out int key))
        {
            if (_tree.Remove(key))
            {
                MessageBox.Show("Record removed successfully.");
                DisplayTree();
            }
            else
            {
                MessageBox.Show("Key not found.");
            }
        }
        else
        {
            MessageBox.Show("Invalid key.");
        }
    }

    private void btnDropDatabase_Click(object sender, EventArgs e)
    {
        _tree.Clear();
        MessageBox.Show("All records cleared.");
        DisplayTree();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtKey.Text, out int key) && !string.IsNullOrWhiteSpace(txtData.Text))
        {
            if (_tree.Update(key, txtData.Text)) // Попытка обновить значение
            {
                MessageBox.Show("Record updated successfully.");
                DisplayTree();
            }
            else
            {
                MessageBox.Show("Key not found. Cannot edit a non-existing key.");
            }
        }
        else
        {
            MessageBox.Show("Invalid input. Please enter a valid key and value.");
        }
    }

    private void btnAddDummyRecords_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtNumRecords.Text, out int count) && count > 0)
        {
            int addedCount = 0; 
            int currentKey = 1; 

            while (addedCount < count)
            {
                if (!_tree.ContainsKey(currentKey)) 
                {
                    _tree.Add(currentKey, $"Value {currentKey}");
                    addedCount++; 
                }

                currentKey++; 
            }

            MessageBox.Show($"{addedCount} new records added.");
            DisplayTree();
        }
        else
        {
            MessageBox.Show("Enter a valid number.");
        }
    }

    private void DisplayTree()
    {
        listBoxRecords.Items.Clear();
        foreach (var pair in _tree)
        {
            listBoxRecords.Items.Add($"{pair.Key}: {pair.Value}");
        }
        SaveDataToFile();
    }

    private void SaveDataToFile()
    {
        _tree.SaveToFile(FileName); 
    }
}