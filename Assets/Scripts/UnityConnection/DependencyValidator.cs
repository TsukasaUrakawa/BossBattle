using System.Collections.Generic;

/// <summary>
/// 初期化の前に、必要参照がそろっているかと、同じ役割の管理者が二重に存在しないかを検査し、不足を「対象の名前」で報告する。
/// </summary>
public class DependencyValidator
{
    /// <summary>
    /// 与えられたNamedBindingのリストを検査し、問題のある名前を返す。
    /// </summary>
    /// <param name="bindings">NamedBindingのリスト</param>
    /// <returns>問題のある名前のリスト</returns>
    public IReadOnlyList<string> ValidateSceneBindings(IReadOnlyList<NamedBinding> bindings)
    {
        // 確認した全ての名前を記録するためのHashSet。重複を検出するために使用する。
        var seenNames = new HashSet<string>();

        // 参照不足、または名前重複のあるNameを集める。
        var invalidNames = new List<string>();

        // すべてのNamedBindingを確認する。
        foreach (var binding in bindings)
        {
            // 名前が既に存在するか、参照がnullの場合は<c>invalidNames</c>に追加する。
            if (!seenNames.Add(binding.Name) || binding.Reference == null)
            {
                // すでに存在する名前を追加しないようにするため、重複をチェックする。
                if (!invalidNames.Contains(binding.Name))
                    invalidNames.Add(binding.Name);
            }
        }
        return invalidNames;
    }
}
