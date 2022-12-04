import React, { useState } from 'react';
import axios from 'axios';
import Header from '../../components/Header';
import { Button } from '@material-ui/core';
export default function ImportMembers() {
  const [selectedFile, setSelectedFile] = useState();
  const [isFilePicked, setIsFilePicked] = useState(false);

  const changeHandler = (event) => {
    setSelectedFile(event.target.files[0]);
    setIsFilePicked(true);
  };

  const handleSubmission = () => {
    const formData = new FormData();

    formData.append('File', selectedFile);

    axios
      .post('https://localhost:7184/api/User/import-members', {
        method: 'POST',
        body: formData,
      })
      .then((result) => {
        console.log('Success:', result);
      })
      .catch((error) => {
        console.error('Error:', error);
      });
  };

  return (
    <>
      <Header />
      <input type="file" name="file" onChange={changeHandler} />
      {isFilePicked ? (
        <div>
          <p>Filename: {selectedFile.name}</p>
          <p>Filetype: {selectedFile.type}</p>
        </div>
      ) : (
        <p>Select a file to show details</p>
      )}
      <div>
        <Button color="primary" onClick={handleSubmission}>
          Submit
        </Button>
      </div>
    </>
  );
}
