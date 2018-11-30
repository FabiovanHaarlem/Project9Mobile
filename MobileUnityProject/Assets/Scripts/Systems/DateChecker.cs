using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateChecker : MonoBehaviour
{
    Holidays m_Holiday;

    private void Awake()
    {
        m_Holiday = Holidays.Normale;
        if (!StaticDataContainer.m_DateChecked)
        {
            CheckChristmass();
        }
    }

    private void CheckChristmass()
    {
        if (CheckMonth() == 11 || CheckMonth() == 12)
        {
            if (CheckDay() > 0 && CheckDay() < 32)
            {
                m_Holiday = Holidays.Christmass; 
            }
        }

        StaticDataContainer.m_Holiday = m_Holiday;
        StaticDataContainer.m_DateChecked = true;
    }

    private int CheckMonth()
    {
        return DateTime.Now.Month;
    }

    private int CheckDay()
    {
        return DateTime.Now.Day;
    }

    public Holidays GetHoliday()
    {
        return StaticDataContainer.m_Holiday;
    }
}

public enum Holidays
{
    Normale = 0,
    Christmass
}
