namespace AdventOfCode25.Helpers.Algorithms.UnionFind;

public class UnionFind<T> where T : notnull
{
	private readonly Dictionary<T, int> _idMap = [];
	private readonly Dictionary<int, T> _valueMap = [];
	private readonly int _size;
	private readonly int[] _sizes;
	private readonly int[] _ids;
	private int _numberOfComponents;

	public UnionFind(IEnumerable<T> nodes)
	{
		var nodeList = nodes.ToList();
		if (nodeList is not { Count: > 0 })
			throw new ArgumentException("Please provide at least one node!", nameof(nodes));

		_size = nodeList.Count;
		_numberOfComponents = nodeList.Count;
		_sizes = Enumerable.Repeat(1, _size).ToArray();
		_ids = Enumerable.Range(0, _size).ToArray();

		foreach (var (node, index) in nodeList.Select((n, i) => (n, i)))
		{
			_idMap.Add(node, index);
			_valueMap.Add(index, node);
		}
	}

	public T Find(T element) => _valueMap[Find(_idMap[element])];

	public bool CheckConnection(T p, T q)
		=> CheckConnection(_idMap[p], _idMap[q]);

	public int GetComponentSize(T p)
		=> _sizes[Find(_idMap[p])];

	public int Size()
		=> _size;

	public int Count()
		=> _numberOfComponents;

	public void Unify(T p, T q)
		=> Unify(_idMap[p], _idMap[q]);

	public T[] GetRoots()
		=> _ids.Where((id, index) => id == index).Select(i => _valueMap[i]).ToArray();

	#region Private helpers

	private int Find(int p)
	{
		var root = p;

		// find root id
		while (root != _ids[root])
			root = _ids[root];

		// compress path
		while (p != root)
		{
			int next = _ids[p];
			_ids[p] = root;
			p = next;
		}

		// return root node for p
		return root;
	}

	private bool CheckConnection(int p, int q)
		=> Find(p) == Find(q);

	private int GetComponentSize(int p)
		=> _sizes[Find(p)];

	private void Unify(int p, int q)
	{
		int rootP = Find(p);
		int rootQ = Find(q);

		if (rootP == rootQ) return;

		if (_sizes[rootP] < _sizes[rootQ])
		{
			_sizes[rootQ] += _sizes[rootP];
			_ids[rootP] = rootQ;
		}
		else
		{
			_sizes[rootP] += _sizes[rootQ];
			_ids[rootQ] = rootP;
		}

		_numberOfComponents--;
	}

	#endregion
}