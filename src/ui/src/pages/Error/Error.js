import React from 'react';

export default function Error() {
  return (
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
      <h1 style={{ fontStyle: 'italic' }}>
        You are not allowed to view this page
      </h1>
    </div>
  );
}
