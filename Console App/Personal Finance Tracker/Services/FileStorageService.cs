using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FinanceTracker.Models;

namespace FinanceTracker.Services;

public class FileStorageService
{
    private readonly string _filePath;

    public FileStorageService(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_filePath, json);
    }

    public List<User> LoadUsers()
    {
        if (!File.Exists(_filePath))
        {
            return new List<User>();
        }

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<User>();
        }

        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
}