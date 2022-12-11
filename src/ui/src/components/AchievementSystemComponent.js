import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';
export default function AchievementSystemComponent({ systems }) {
  return (
    <Card
      sx={{
        display: 'flex',
        width: '70%',
        margin: '1%',
        padding: '5%',
        alignItems: 'center',
        justifyContent: 'space-between',
      }}
    >
      <CardMedia
        component="img"
        alt="green iguana"
        height="140"
        image="/static/media/image.44622c8043ea62aac81b.jpg"
        sx={{
          float: 'left',
          margin: '0 1.5%',
          width: '30%',
          backgroundSize: '100% 100%',
        }}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {systems.name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {systems.description}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small">
          <Link to={`/system-achievements?id=${systems.id}`}>View Details</Link>
        </Button>
      </CardActions>
    </Card>
  );
}
