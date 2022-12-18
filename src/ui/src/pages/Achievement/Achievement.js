import React, { useState, useEffect } from 'react';
import Header from '../../components/Header';
import axios from 'axios';
import AchievementComponent from '../../components/AchievementComponent';
import { Button } from '@mui/material';
import { Link, useSearchParams } from 'react-router-dom';
const BASE_URL = `https://localhost:7184/api`;
export default function Achievement() {
  const [data, setData] = useState();
  const [isLoading, setLoading] = useState(false);
  const [searchParams, setSearchParams] = useSearchParams();

  useEffect(() => {
    axios
      .get(BASE_URL + `/AchievementSystem`, {
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + StorageUser.token,
        },
      })
      .then(({ data }) => {
        console.log(data);
        setLoading(true);
        setData(data);
      });
  }, []);
  let StorageUser = JSON.parse(sessionStorage.getItem('user'));
  return (
    <>
      <Header />
      <div className="div" style={{ margin: '1%' }}>
        <Link
          to={`/create-achievements?id=${searchParams.get('id')}`}
          style={{ textDecoration: 'none' }}
        >
          <Button variant="contained">ADD NEW ACHIEVEMENT</Button>
        </Link>
      </div>
      <div
        className="achiv-systems"
        style={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          height: '100%',
        }}
      >
        {data
          ? data.map((systems) =>
              systems.achievements.map((achievement) => (
                <AchievementComponent
                  key={achievement.id}
                  achievement={achievement}
                />
              ))
            )
          : ''}
      </div>
    </>
  );
}
