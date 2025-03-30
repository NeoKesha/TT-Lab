using TT_Lab.ViewModels.Editors;

namespace TT_Lab.Services.Implementations;

public class ActiveChunkService : IActiveChunkService
{
    private ChunkEditorViewModel? _currentChunkEditor;

    public ChunkEditorViewModel? CurrentChunkEditor => _currentChunkEditor;

    public void SetCurrentChunkEditor(ChunkEditorViewModel? chunkEditor)
    {
        _currentChunkEditor = chunkEditor;
    }
}