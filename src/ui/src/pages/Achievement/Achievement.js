import React, { useState, useEffect } from 'react';
import Header from '../../components/Header';
import axios from 'axios';
const BASE_URL = `https://localhost:7184/api`;
export default function Achievement() {
  const [data, setData] = useState();
  const [isLoading, setLoading] = useState(false);

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
              systems.achievements.map((achiv) => (
                <div key={achiv.id}>
                  <p>{achiv.name}</p>
                </div>
              ))
            )
          : ''}
      </div>
    </>
  );
}
