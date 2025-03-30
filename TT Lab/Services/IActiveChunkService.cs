using TT_Lab.ViewModels.Editors;

namespace TT_Lab.Services;

public interface IActiveChunkService
{
    ChunkEditorViewModel? CurrentChunkEditor { get; }

    void SetCurrentChunkEditor(ChunkEditorViewModel? chunkEditor);
}