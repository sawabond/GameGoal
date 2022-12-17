import React from 'react';
import Header from '../../components/Header';
export default function Error() {
  return (
    <>
      <Header />
      <div
        className="error"
        style={{
          backgroundColor: '#bec1d0',
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          width: '100%',
          height: '100vh',
        }}
      >
        <h1>403 | You are not allowed to view this page</h1>
      </div>
    </>
  );
}
