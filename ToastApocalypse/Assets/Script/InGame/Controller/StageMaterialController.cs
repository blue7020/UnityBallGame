using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaterialController : MonoBehaviour
{
    public static StageMaterialController Instance;

    public MaterialObj[] mMaterialArr;
    public MaterialObj[] mStageMaterialArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mStageMaterialArr = new MaterialObj[5];
        }
        else
        {
            Delete();
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        if (GameController.Instance.GotoMain == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GetMaterialArr(int StageId)
    {
        switch (StageId)
        {
            case 1:
                for (int i=0; i< mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[3];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[2];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[4];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[1];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[8];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[12];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[10];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[4];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[6];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[5];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[7];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[1];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 4:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[15];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[8];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[4];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[9];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[13];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[2];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[12];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[14];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 6:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.3f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[3];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[6];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[11];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[1];
                            mStageMaterialArr[i].mRate = 0.05f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 7:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.25f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[16];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[17];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[11];
                            mStageMaterialArr[i].mRate = 0.15f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[7];
                            mStageMaterialArr[i].mRate = 0.1f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
            case 8:
                for (int i = 0; i < mStageMaterialArr.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            mStageMaterialArr[i] = mMaterialArr[0];
                            mStageMaterialArr[i].mRate = 0.15f;
                            break;
                        case 1:
                            mStageMaterialArr[i] = mMaterialArr[18];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 2:
                            mStageMaterialArr[i] = mMaterialArr[14];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 3:
                            mStageMaterialArr[i] = mMaterialArr[13];
                            mStageMaterialArr[i].mRate = 0.25f + GameController.Instance.MaterialDropRate;
                            break;
                        case 4:
                            mStageMaterialArr[i] = mMaterialArr[7];
                            mStageMaterialArr[i].mRate = 0.1f + GameController.Instance.MaterialDropRate;
                            break;
                    }
                }
                break;
        }
    }
}
