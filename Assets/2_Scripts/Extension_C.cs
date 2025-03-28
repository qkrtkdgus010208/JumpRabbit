public static class Extension_C
{
    private const string format0N0 = "{0:N0}"; // �Ҽ��� 0�ڸ� ����
    private const string format0N1 = "{0:N1}"; // �Ҽ��� 1�ڸ� ����
    private const string format0N2 = "{0:N2}"; // �Ҽ��� 2�ڸ� ����
    private const string format0N3 = "{0:N3}"; // �Ҽ��� 3�ڸ� ����

    // float ���� ���ڿ��� ��ȯ�ϴ� Ȯ�� �޼���
    public static string ToString_Func(this float _value, int _pointNumber = 0)
    {
        if (_pointNumber > 0)
        {
            if (_pointNumber == 1)
                return string.Format(format0N1, _value); // �Ҽ��� 1�ڸ� �������� ��ȯ
            else if (_pointNumber == 2)
                return string.Format(format0N2, _value); // �Ҽ��� 2�ڸ� �������� ��ȯ
            else if (_pointNumber == 3)
                return string.Format(format0N3, _value); // �Ҽ��� 3�ڸ� �������� ��ȯ
            else
                return _value.ToString(); // �⺻ �������� ��ȯ
        }
        else
        {
            return string.Format(format0N0, _value); // �Ҽ��� 0�ڸ� �������� ��ȯ
        }
    }

    // float ���� �ۼ�Ʈ ���ڿ��� ��ȯ�ϴ� Ȯ�� �޼���
    public static string ToString_Percent_Func(this float _value)
    {
        return (_value * 100f).ToString_Func() + "%"; // �ۼ�Ʈ �������� ��ȯ
    }
}


