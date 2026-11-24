using UnityEngine;

/// <summary>
/// DependencyValidatorへ渡す1件分のデータ。表示用の名前と、検査対象の参照を持つ
/// 生成後に変更しないデータのため readonly struct
/// </summary>
public readonly struct NamedBinding
{
    /// <summary>
    /// 不足したときにそのまま表示する名前
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// nullかどうかを検査する対象
    /// </summary>
    public Object Reference { get; }

    public NamedBinding(string name, Object reference)
    {
        Name = name;
        Reference = reference;
    }
}
