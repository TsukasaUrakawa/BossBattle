using UnityEngine;
using System.Collections.Generic;

public class GameCompositionRoot : MonoBehaviour
{
    /// <summary>
    /// プレイヤーオブジェクトへの参照。
    /// </summary>
    [SerializeField] private GameObject _playerObject;

    /// <summary>
    /// ボスオブジェクトへの参照。
    /// </summary>
    [SerializeField] private GameObject _bossObject;

    /// <summary>
    /// プレイヤーの出現位置への参照。
    /// </summary>
    [SerializeField] private Transform _playerSpawn;

    /// <summary>
    /// ボスの出現位置への参照。
    /// </summary>
    [SerializeField] private Transform _bossSpawn;

    /// <summary>
    /// 初期化が完了したかどうかを示す。
    /// </summary>
    public bool IsInitialized { get; private set; }

    void Awake()
    {
        Initialize();
    }

    void OnDestroy()
    {
        Dispose();
    }

    /// <summary>
    /// 初期化処理を行うメソッド。依存関係の検証を行い、問題がなければ初期化済みフラグを立てる。
    /// </summary>
    void Initialize()
    {
        // すでに初期化済みの場合は、再度初期化しないようにする。
        if (IsInitialized)
        {
            Debug.Log("既に初期化されています。");
            return;
        }
        
        DependencyValidator dependencyValidator = new DependencyValidator();

        // 依存関係の検証を行い、問題のある名前を取得する。
        var invalidNames = dependencyValidator.ValidateSceneBindings(new List<NamedBinding>()
        {
            new NamedBinding("PlayerObject", _playerObject),
            new NamedBinding("BossObject", _bossObject),
            new NamedBinding("PlayerSpawn", _playerSpawn),
            new NamedBinding("BossSpawn", _bossSpawn)
        });

        // 問題のある名前が存在する場合は、エラーログを出力して初期化を中止する。
        if (invalidNames.Count > 0)
        {
            foreach (var invalidName in invalidNames)
            {
                Debug.LogError($"参照検査失敗: {invalidName} 参照不足または重複があります。");
            }
            return;
        }

        // 初期化成功
        IsInitialized = true;
        Debug.Log("GameCompositionRootの初期化が完了しました。");
    }

    /// <summary>
    /// 初期化済みフラグをリセットする。
    /// </summary>
    void Dispose()
    {
        IsInitialized = false;
    }
}
