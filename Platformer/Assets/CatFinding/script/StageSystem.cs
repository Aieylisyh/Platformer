using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StageSystem : MonoBehaviour
{
    public TileData[] level_1_data;

    public TileData playerSpawnData;
    public TileData catSpawnData;
    TileData _playerData = new TileData();
    TileData _catData = new TileData();

    bool _playerIsMoving;//如果玩家正在移动中，禁止操作

    public Vector2Int[] catRoutine;
    int _catRoutineIndex;

    public float stepTime;

    private void Start()
    {
        foreach (var t in level_1_data)
        {
            Instantiate(t.g, new Vector3(t.p.x, t.p.y, 0), Quaternion.identity);

            if (t.p == playerSpawnData.p)
            {
                _playerData.g = Instantiate(playerSpawnData.g, new Vector3(t.p.x, t.p.y, 0), Quaternion.identity);
                _playerData.p = playerSpawnData.p;
                _playerIsMoving = false;
            }

            if (t.p == catSpawnData.p)
            {
                _catData.g = Instantiate(catSpawnData.g, new Vector3(t.p.x, t.p.y, 0), Quaternion.identity);
                _catData.p = catSpawnData.p;
            }
        }

        TestCoroutine();
    }

    private void Update()
    {
        if (!_playerIsMoving)
        {
            if (Input.GetKeyDown("w"))
                GoToNearbyTile(0, 1, true);//go up

            if (Input.GetKeyDown("s"))
                GoToNearbyTile(0, -1, true); //go down

            if (Input.GetKeyDown("a"))
                GoToNearbyTile(-1, 0, true);  //go left

            if (Input.GetKeyDown("d"))
                GoToNearbyTile(1, 0, true); //go right
        }
        if (Input.GetMouseButtonDown(0))
            catCanMove = !catCanMove;
    }

    public bool catCanMove;
    void GoToNearbyTile(int x, int y, bool isPlayerOrCat)
    {
        GoToNearbyTile(new Vector2Int(x, y), isPlayerOrCat);
    }

    void GoToNearbyTile(Vector2Int pos, bool isPlayerOrCat)
    {
        if (isPlayerOrCat)
        {
            GoToTile(_playerData.p + pos, isPlayerOrCat);
        }
        else
        {
            GoToTile(_catData.p + pos, isPlayerOrCat);
        }
    }

    void GoToTile(Vector2Int pos, bool isPlayerOrCat)
    {
        foreach (var t in level_1_data)
        {
            if (t.p == pos)
            {
                if (isPlayerOrCat)
                {
                    _playerData.p = pos;
                    //player.transform.position = new Vector3(t.x, t.y, 0);
                    _playerData.g.transform.DOMove(new Vector3(pos.x, pos.y, 0), stepTime).
                    OnComplete(ResumePlayerControl);

                    _playerIsMoving = true;

                    //here write code for the reaction of the other game elements
                    //DoCatReaction();
                }
                else
                {
                    _catData.p = pos;
                    _catData.g.transform.DOMove(new Vector3(pos.x, pos.y, 0), stepTime);
                }


                break;
            }
        }
    }

    void ResumePlayerControl()
    {
        _playerIsMoving = false;
    }

    void DoCatReaction()
    {
        if (_catRoutineIndex >= catRoutine.Length)
        {
            _catRoutineIndex = 0;
        }

        GoToNearbyTile(catRoutine[_catRoutineIndex].x, catRoutine[_catRoutineIndex].y, false);
        _catRoutineIndex = _catRoutineIndex + 1;
    }

    void TestCoroutine()
    {
        StartCoroutine(CatMoveStepByStep());
    }

    IEnumerator CatMoveStepByStep()
    {
        while (true)
        {
            if (catCanMove)
            {
                DoCatReaction();
                yield return new WaitForSeconds(0.36f);
            }
            else
                yield return null;
        }
    }
}