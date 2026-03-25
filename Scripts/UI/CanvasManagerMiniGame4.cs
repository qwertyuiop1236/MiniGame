using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConnectDotsMobile : MiniGameManager
{
    // ====== Синглтон ======
    public static ConnectDotsMobile MiniGame4 { get; private set; }

    [Header("Списки клеток путей (порядок = порядок нажатия)")]
    [SerializeField] private Button[] path1Cells;
    [SerializeField] private Button[] path2Cells;
    [SerializeField] private Button[] path3Cells;
    [SerializeField] private Button[] path4Cells;

    [Header("Цвета подсветки путей")]
    [SerializeField] private Color colorPath1Cells;
    [SerializeField] private Color colorPath2Cells;
    [SerializeField] private Color colorPath3Cells;
    [SerializeField] private Color colorPath4Cells;

    [Header("Базовый цвет и цвет завершённой клетки")]
    [SerializeField] private Color colorBase;
    [SerializeField] private Color completedColor = Color.green;

    [Header("Настройки подсветки")]
    [SerializeField] private float highlightTime = 3f;

    // --- Состояния ---
    private bool _isHighlighting = true;
    private float _highlightTimer;

    // Текущий путь (список кнопок), индекс и флаг завершения
    private List<Button> _currentPathButtons;
    private int _currentPathIndex;
    private bool _pathCompleted;

    // 👇 Удобный список ВСЕХ путей для итерации
    private List<Button[]> _allPaths;

    protected override void Start()
    {
        base.Start();

        MiniGame4 = this;

        _allPaths = new List<Button[]>
        {
            path1Cells, path2Cells, path3Cells, path4Cells
        };

        StartHighlight();
    }

    protected override void Update()
    {
        if (!_IsEnd)
        {
            base.Update();
            if (_isHighlighting)
            {
                _highlightTimer += Time.deltaTime;
                if (_highlightTimer >= highlightTime)
                    EndHighlight();
            }
        }
    }

    // --- Подсветка и интерактивность ---
    private void BacklightButton(Button[] cells, Color targetColor)
    {
        if (cells == null) return;
        foreach (var btn in cells)
        {
            if (btn == null) continue;
            var img = btn.GetComponent<Image>();
            if (img != null) img.color = targetColor;
        }
    }

    private void StartHighlight()
    {
        _isHighlighting = true;
        _highlightTimer = 0f;
        BacklightButton(path1Cells, colorPath1Cells);
        BacklightButton(path2Cells, colorPath2Cells);
        BacklightButton(path3Cells, colorPath3Cells);
        BacklightButton(path4Cells, colorPath4Cells);
        SetAllButtonsInteractable(false);
    }

    private void ClearAllHighlights()
    {
        BacklightButton(path1Cells, colorBase);
        BacklightButton(path2Cells, colorBase);
        BacklightButton(path3Cells, colorBase);
        BacklightButton(path4Cells, colorBase);
    }

    private void SetAllButtonsInteractable(bool interactable)
    {
        foreach (var path in _allPaths)
        {
            if (path == null) continue;
            foreach (var btn in path)
                if (btn != null) btn.interactable = interactable;
        }
    }

    private void EndHighlight()
    {
        _isHighlighting = false;
        ClearAllHighlights();
        SetAllButtonsInteractable(true);
        Debug.Log("Подсветка завершена. Можно играть!");
    }

    // --- Главный метод, вызываемый кнопками ---
    public void RegisterCellClick(Button clickedButton)
    {
        // ----- Защита от ошибок -----
        if (_IsEnd || _isHighlighting || clickedButton == null)
        {
            Debug.Log("Клик проигнорирован (игра закончена / подсветка / null).");
            return;
        }

        // 1. Запрещаем кликать по уже пройденным (зелёным) клеткам
        var img = clickedButton.GetComponent<Image>();
        if (img != null && img.color == completedColor)
        {
            PlayErrorSound();
            Debug.Log("Клетка уже пройдена!");
            return;
        }

        // 2. Запрещаем кликать по неинтерактивным кнопкам (хотя по идее они не должны быть кликабельны)
        if (!clickedButton.interactable)
        {
            PlayErrorSound();
            Debug.Log("Кнопка неактивна!");
            return;
        }

        // ----- Если ещё не начали путь -----
        if (_currentPathButtons == null)
        {
            // Определяем, к какому пути принадлежит кнопка
            Button[] selectedPath = null;
            if (path1Cells != null && path1Cells.Contains(clickedButton))
                selectedPath = path1Cells;
            else if (path2Cells != null && path2Cells.Contains(clickedButton))
                selectedPath = path2Cells;
            else if (path3Cells != null && path3Cells.Contains(clickedButton))
                selectedPath = path3Cells;
            else if (path4Cells != null && path4Cells.Contains(clickedButton))
                selectedPath = path4Cells;

            if (selectedPath == null)
            {
                Debug.LogError("Кнопка не принадлежит ни одному пути!");
                PlayErrorSound();
                return;
            }

            _currentPathButtons = selectedPath.ToList();
            _currentPathIndex = 0;
            _pathCompleted = false;
            Debug.Log($"Начат путь. Всего клеток: {_currentPathButtons.Count}");
        }

        // ----- Проверка порядка нажатия -----
        if (_currentPathIndex >= _currentPathButtons.Count)
        {
            PlayErrorSound();
            Debug.Log("Путь уже пройден!");
            return;
        }

        if (_currentPathButtons[_currentPathIndex] != clickedButton)
        {
            PlayErrorSound();
            Debug.Log($"Ошибка: нужно нажать клетку {_currentPathButtons[_currentPathIndex].name}");
            return;
        }

        // ----- Всё верно – отмечаем клетку -----
        if (img != null) img.color = completedColor;
        PlayClickSound();
        _currentPathIndex++;
        Debug.Log($"Прогресс: {_currentPathIndex}/{_currentPathButtons.Count}");

        // ----- Проверка завершения пути -----
        if (_currentPathIndex >= _currentPathButtons.Count)
        {
            _pathCompleted = true;

            // Отключаем кнопки этого пути, чтобы нельзя было нажать снова
            foreach (var btn in _currentPathButtons)
                if (btn != null) btn.interactable = false;

            CheckForPathCompletion();
        }
    }

    // --- Завершение текущего пути ---
    private void CheckForPathCompletion()
    {
        if (_pathCompleted && _currentPathButtons != null)
        {
            Debug.Log($"✅ Путь завершён! Клеток: {_currentPathButtons.Count}");
            
            _currentPathButtons = null;
            _currentPathIndex = 0;
            _pathCompleted = false;

            CheckAllPathsCompleted();
        }
    }

    // --- Проверка, все ли 4 пути полностью зелёные ---
    private void CheckAllPathsCompleted()
    {
        bool allDone = true;

        foreach (var path in _allPaths)
        {
            if (path == null || path.Length == 0) continue;

            foreach (var btn in path)
            {
                if (btn == null) continue;
                var img = btn.GetComponent<Image>();
                // Смотрим: если цвет не зеленый, путь не завершен
                if (img != null && img.color != completedColor)
                {
                    allDone = false;
                    break;
                }
            }
            if (!allDone) break;
        }

        if (allDone)
        {
            _IsEnd = true;
            Invoke(nameof(Win), 1f);
            Debug.Log("🎉 ВСЕ ПУТИ ПРОЙДЕНЫ! ПОБЕДА!");
        }
    }

    // --- Сброс текущего незавершённого пути ---
    public void ResetCurrentPath()
    {
        if (_IsEnd || _isHighlighting) return;

        if (_currentPathButtons != null)
        {
            foreach (var btn in _currentPathButtons)
            {
                if (btn == null) continue;
                var img = btn.GetComponent<Image>();
                if (img != null) img.color = colorBase;
                btn.interactable = true;  // Возвращаем интерактивность
            }

            _currentPathButtons = null;
            _currentPathIndex = 0;
            _pathCompleted = false;
        }

        PlayClickSound();
    }

    // --- Звуки с защитой ---
    private void PlayErrorSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayErrorSound();
    }

    private void PlayClickSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
    }

    protected override void Win()
    {
        SavingSystem.LevelSeve(4,indexLeyer);
        base.Win();
    }

    protected override void Defeat()
    {
        base.Defeat();
    }

    protected override void SceneHome()
    {
        base.SceneHome();
    }
    
}