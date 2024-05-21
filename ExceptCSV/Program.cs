using System.Text;
internal class Program
{

    private static void Main(string[] args)
    {
        ReadCSVAndCreateNewList();
    }

    /// <summary>
    /// ファイルの作成
    /// </summary>
    private static void CreateCSVFile()
    {
        //日本語の文字コードを使えるようにするプロバイダーの設定
        EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        var encoding = provider.GetEncoding(932);

        //ファイルの中身を保存するための変数     //改行コード
        var body = "名前,年齢,性別,学部,役職" + Environment.NewLine + "新井,19,女,文学部,なし\n八木,22,男,経営学部,童貞";

        //ファイルを保存する処理(第一引数にパス、第二引数にテキストの中身、第三引数が文字コードの設定)
        System.IO.File.WriteAllText("D:\\Programing\\C#\\CSharpTutorial\\csv1\\ext.csv", body, encoding);
        Console.WriteLine("保存しました");

    }

    /// <summary>
    /// ファイルの読み取って名前だけ抽出したファイルを作成
    /// </summary>
    /// <param name="args"></param>
    private static void ReadCSVAndCreateNameList()
    {
        EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        var encoding = provider.GetEncoding(932);

        //ReadAllTextでファイルを読み取る(第一引数に読み込むファイルのパス、第二引数に文字コードの設定)
        var body = System.IO.File.ReadAllText("D:\\Programing\\C#\\CSharpTutorial\\member.csv", encoding);

        //ファイルを一行ずつ読み込ませるインスタンスの呼び出し
        var sr = new StringReader(body);

        var nameList = new String[3];
        var index = 0;
        //一行ずつ読み込んで出力
        while (sr.Peek() > -1)   //Peekメソッドで次の文字の文字コードを取得(次の文字がない場合は-1を返す)(-1になったらfalseになる)
        {
            string line = sr.ReadLine();    //ReadLineで一行ずつ読み込んでlineに代入
            if (index == 0)
            {
                index++;
                continue;   //一行目をスキップして項目名を飛ばす
            }
            var nameCount = line.IndexOf(',');  //IndexOfメソッドでカンマの位置までの文字数を数える
            nameList[index - 1] = line.Substring(0, nameCount); //Substringメソッドで配列の0~名前の文字数分を取得
            Console.WriteLine(nameList[index - 1]);
            index++;
        }

        //memberから名前を抽出したファイルを新たに作成
        var newBody = "";
        foreach (var name in nameList)
        {
            newBody += name + Environment.NewLine;
        }

        System.IO.File.WriteAllText("D:\\Programing\\C#\\CSharpTutorial\\ConvertCSV\\name.csv", newBody, encoding);

        Console.WriteLine("終了");
    }

    /// <summary>
    /// ファイルを読み取って名前と学部を抽出したファイルを作成
    /// </summary>
    private static void ReadCSVAndCreateNewList()
    {
        EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
        var encoding = provider.GetEncoding(932);

        //ReadAllTextでファイルを読み取る(第一引数に読み込むファイルのパス、第二引数に文字コードの設定)
        var body = System.IO.File.ReadAllText("D:\\Programing\\C#\\CSharpTutorial\\member.csv", encoding);

        //ファイルを一行ずつ読み込ませるインスタンスの呼び出し
        var sr = new StringReader(body);
        var nameList = new String[3];
        var gakubuList = new String[3];
        bool isSkip = true;

        var index = 0;
        //一行ずつ読み込んで出力
        while (sr.Peek() > -1)   //Peekメソッドで次の文字の文字コードを取得(次の文字がない場合は-1を返す)(-1になったらfalseになる)
        {
            string line = sr.ReadLine();    //ReadLineで一行ずつ読み込んでlineに代入
            if (isSkip)                     //一行目をスキップして項目名を飛ばす処理
            {
                isSkip = false;
                continue;
            }

            var ss = line.Split(',');   //カンマで区切ってss配列にそれぞれ代入

            nameList[index] = ss[0];    //名前（配列の0番目）を代入
            gakubuList[index] = ss[3];  //学部(配列の3番目)を代入

            Console.Write(nameList[index]);
            Console.WriteLine(gakubuList[index]);
            index++;
        }

        //memberから名前と学部を抽出したファイルを新たに作成
        var newBody = "";
        for (int i = 0; i < nameList.Length; i++)
        {
            newBody += nameList[i] + "," + gakubuList[i] + Environment.NewLine;
        }

        System.IO.File.WriteAllText("D:\\Programing\\C#\\CSharpTutorial\\ReadCSVAndCreateNewList\\newList.csv", newBody, encoding);

        Console.WriteLine("終了");
    }
}
